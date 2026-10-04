using UnityEngine;
using UnityEngine.EventSystems;

// needs a Collider2D on this object and a Physics2DRaycaster on the camera
public class RadioInput : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public enum Action { Rub, Screw, Knob, Power }

    [SerializeField] private Radio radio;
    [SerializeField] private Action action;

    public void OnDrag(PointerEventData e)
    {
        print($"dragging {action} {e.delta}");
        if (action == Action.Rub) radio.Rub(e.delta.magnitude);
        if (action == Action.Screw) radio.Screw(DragAngle(e));
        if (action == Action.Knob) radio.TurnKnob(DragAngle(e));
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (action == Action.Power) radio.TogglePower();
    }

    // clockwise degrees dragged around this object
    private float DragAngle(PointerEventData e)
    {
        Vector2 center = e.pressEventCamera.WorldToScreenPoint(transform.position);
        return -Vector2.SignedAngle(e.position - e.delta - center, e.position - center);
    }
}