using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform _dragRectTransform;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
}
