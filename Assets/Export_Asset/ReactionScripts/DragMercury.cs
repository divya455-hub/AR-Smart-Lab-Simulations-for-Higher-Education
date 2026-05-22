using UnityEngine;
using UnityEngine.EventSystems;

public class DragMercury : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        parentAfterDrag = transform.parent;
        transform.SetParent(parentAfterDrag.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 5f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
    }
}
