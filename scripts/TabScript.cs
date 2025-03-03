using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    public RectTransform targetRect;
    private bool isDragging = false;
    private Vector3 offset;
    private desktopManager manager;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        manager = GameObject.FindWithTag("GameController").GetComponent<desktopManager>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        offset = rectTransform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        manager.tabFrontier(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
            rectTransform.position = ClampPositionInTargetRect(newPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private Vector3 ClampPositionInTargetRect(Vector3 position)
    {
        Vector3 clampedPosition = position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, targetRect.rect.xMin, targetRect.rect.xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, targetRect.rect.yMin, targetRect.rect.yMax);
        return clampedPosition;
    }
}