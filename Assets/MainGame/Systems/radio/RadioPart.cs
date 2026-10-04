using UnityEngine;
using UnityEngine.EventSystems;
public enum PartType { Dust, Screw, Knob, Power }

// put on each part of the radio with a Collider2D
public class RadioPart : MonoBehaviour, IToolTarget, IDragHandler, IPointerClickHandler
{

    [SerializeField] private Radio radio;
    [SerializeField] private PartType part;

    public bool Accepts(ToolType tool)
    {
        return  part == PartType.Screw && tool == ToolType.Screwdriver;
    }

    public void Use(ToolType tool, float amount)
    {
        if (part == PartType.Screw) radio.Screw(amount);
    }

    // knob and power are used by hand
    public void OnDrag(PointerEventData e)
    {
        if (part == PartType.Knob) radio.TurnKnob(Tool.DragAngle(e, transform.position));
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (part == PartType.Power) radio.TogglePower();
    }
}