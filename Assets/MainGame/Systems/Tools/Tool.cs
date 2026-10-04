using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum ToolType { Cloth, Screwdriver }


public interface IToolTarget
{
    bool Accepts(ToolType tool);
    void Use(ToolType tool, float amount);
}


public class Tool : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ToolType type;
 
    private IToolTarget target;
    private Transform targetTransform;
    private Vector3 startPosition;
 
    public void OnBeginDrag(PointerEventData e)
    {
        startPosition = transform.position;
    }
 
    public void OnDrag(PointerEventData e)
    {
        Vector3 pos = e.pressEventCamera.ScreenToWorldPoint(e.position);
        pos.z = transform.position.z;
        transform.position = pos;
 
        if (target == null) return;
 
        if (type == ToolType.Cloth) target.Use(type, e.delta.magnitude);
        else target.Use(type, DragAngle(e, targetTransform.position));
    }
 
    public void OnEndDrag(PointerEventData e)
    {
        transform.position = startPosition;
        target = null;
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IToolTarget t) && t.Accepts(type))
        {
            target = t;
            targetTransform = other.transform;
        }
    }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == targetTransform) target = null;
    }
 
    // clockwise degrees the pointer moved around a point
    public static float DragAngle(PointerEventData e, Vector3 worldCenter)
    {
        Vector2 center = e.pressEventCamera.WorldToScreenPoint(worldCenter);
        return -Vector2.SignedAngle(e.position - e.delta - center, e.position - center);
    }
}
 