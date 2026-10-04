using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Radio : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField] private PartData antennaData;
    [SerializeField] private ToyData testToy;

    [Header("Visuals")]
    [SerializeField] private Transform antenna;
    [SerializeField] private float antennaDepth = 0.5f; // how far it sinks when screwed in
    [SerializeField] private Transform knob;

    [Header("Dial")]
    [SerializeField] private TMP_Text frequencyText;
    [SerializeField] private Transform needle;
    [SerializeField] private float needleMinAngle = 60f;
    [SerializeField] private float needleMaxAngle = -60f;
    [SerializeField] private SpriteRenderer signalLight;
    [SerializeField] private Color lightOff = new Color(0.2f, 0.2f, 0.2f);
    [SerializeField] private Color lightNoSignal = Color.red;
    [SerializeField] private Color lightTuned = Color.green;

    [Header("Repair")]
    [SerializeField] private float turnsToTighten = 2f;

    [Header("Tuning (MHz)")]
    [SerializeField] private float minFrequency = 87.5f;
    [SerializeField] private float maxFrequency = 108f;
    [SerializeField] private float targetFrequency = 98.4f;
    [SerializeField] private float searchRange = 2f;
    [SerializeField] private float mhzPerTurn = 4f;

    // hook the audio here: IsOn, HasSignal, Tuning
    public UnityEvent<Radio> OnChanged;
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;
    public UnityEvent<float> OnTuned;
    public UnityEvent<float> OnUntuned;
    

    public bool IsOn { get; private set; }
    public float Frequency { get; private set; } = 90f;
    public bool HasSignal => IsOn && antennaPart != null && antennaPart.IsConnected;

    // 0 = only crackles, 1 = clear station
    public float Tuning => HasSignal ? Mathf.Clamp01(1f - Mathf.Abs(Frequency - targetFrequency) / searchRange) : 0f;

    private Part antennaPart;
    private Vector3 antennaLoosePosition;

    private void Awake()
    {
        // place the antenna in the editor where it sits when fully loose
        antennaLoosePosition = antenna.localPosition;
    }

    private void Start()
    {
        if (testToy == null) return;

        var toy = new Toy();
        toy.InitializeCurrentState(testToy);
        Load(toy);
    }

    // wire TicketSystem.OnTicketAccepted to this
    public void LoadTicket(TicketItem ticket)
    {
        ticket.InitializeCurrentState();
        Load(ticket.currentToyState);
    }

    public void Load(Toy toy)
    {
        antennaPart = toy.GetPart(antennaData);

        // every part component finds its own part by its PartData
        foreach (var part in GetComponentsInChildren<IPart>(true))
        {
            part.Initialize(toy.GetPart(part.Data));
        }

        Refresh();
    }

    public void Screw(float degrees)
    {
        if (antennaPart == null) return;
        antennaPart.connected = Mathf.Clamp01(antennaPart.connected + degrees / (turnsToTighten * 360f));
        if (HasSignal && Mathf.Abs(Frequency - targetFrequency) <= searchRange) OnTuned?.Invoke(Frequency);
        else OnUntuned?.Invoke(Frequency);
        Refresh();
    }

    public void TurnKnob(float degrees)
    {
        Frequency = Mathf.Clamp(Frequency + degrees / 360f * mhzPerTurn, minFrequency, maxFrequency);
        if (HasSignal && Mathf.Abs(Frequency - targetFrequency) <= searchRange) OnTuned?.Invoke(Frequency);
        else OnUntuned?.Invoke(Frequency);

        Refresh();
    }

    public void TogglePower()
    {
        IsOn = !IsOn;
        if (IsOn) OnActivated?.Invoke();
        else OnDeactivated?.Invoke();
        Refresh();
    }

    private void Refresh()
    {
        if (antennaPart != null)
        {
            // spin and sink at the same time, like a screw going in
            float connected = antennaPart.connected;
            antenna.localRotation = Quaternion.Euler(0f, (1f - connected) * turnsToTighten * 360f, 0f);
            antenna.localPosition = antennaLoosePosition + Vector3.down * (connected * antennaDepth);
            
        }

        knob.localRotation = Quaternion.Euler(0f, 0f, -(Frequency - minFrequency) / mhzPerTurn * 360f);

        float dial = Mathf.InverseLerp(minFrequency, maxFrequency, Frequency);
        needle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(needleMinAngle, needleMaxAngle, dial));

        frequencyText.text = IsOn ? $"{Frequency:0.0} MHz" : "";

        if (!IsOn) signalLight.color = lightOff;
        else signalLight.color = Color.Lerp(lightNoSignal, lightTuned, Tuning);

        OnChanged?.Invoke(this);
    }
}