using UnityEngine;

public class Cleanliness : MonoBehaviour, IPart
{
    [SerializeField] private PartData data;
    [SerializeField] private float rubToClean = 1000f; // per spot

    private DustSpot[] spots;
    private Part part;

    public PartData Data => data;

    public void Initialize(Part part)
    {
        this.part = part;
        spots = GetComponentsInChildren<DustSpot>(true);
        if (part == null) return;

        foreach (var spot in spots)
        {
            spot.SetClean(part.cleanliness);
        }
    }

    public void Rub(DustSpot spot, float amount)
    {
        if (part == null) return;

        float clean = spot.Clean + amount / rubToClean;
        if (clean >= ToyData.CleanlinessThreshold) clean = 1f; // wipe leftovers
        spot.SetClean(clean);

        // the part is only as clean as its dirtiest spot
        float dirtiest = 1f;
        foreach (var s in spots)
        {
            dirtiest = Mathf.Min(dirtiest, s.Clean);
        }
        part.cleanliness = dirtiest;
    }
}