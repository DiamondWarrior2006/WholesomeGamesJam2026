using UnityEngine;
using UnityEngine.EventSystems;

public class SnapTo : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped on Slot");

        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            RectTransform draggedRectTranform = draggedObject.GetComponent<RectTransform>();
            draggedRectTranform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
