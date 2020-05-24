using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeblockAttacher : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private UILineRenderer _lineRenderer;
    private Canvas _canvas;

    public CodeblockReceiver attachedReceiver;

    private void Start()
    {
        _lineRenderer = GetComponentInChildren<UILineRenderer>();
        _canvas = FindObjectOfType<Canvas>();
    }

    private void Update()
    {
        if (attachedReceiver)
        {
            // Draw the line from the attacher to the receiver
            float xDistance = attachedReceiver.gameObject.transform.position.x - transform.position.x;
            float yDistance = attachedReceiver.gameObject.transform.position.y - transform.position.y;
            Vector2 linePosition = new Vector2(xDistance / _canvas.scaleFactor, yDistance / _canvas.scaleFactor);

            _lineRenderer.Points[1] = linePosition;

            _lineRenderer.SetVerticesDirty();
        }
    }

    public void Attach(CodeblockReceiver receiver)
    {
        if (receiver.AllowMultipleReceivers || receiver.attacher == null)
        {
            attachedReceiver = receiver;
            receiver.SetAttacher(this);
        } else
        {
            Detach();
        }
    }

    public void Detach()
    {
        if (attachedReceiver)
        {
            attachedReceiver.RemoveAttacher();
        }
        attachedReceiver = null;

        _lineRenderer.Points[1] = Vector2.zero;
        _lineRenderer.SetVerticesDirty();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Detach();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float xDistance = Input.mousePosition.x - transform.position.x;
        float yDistance = Input.mousePosition.y - transform.position.y;
        _lineRenderer.Points[1] = new Vector2(xDistance / _canvas.scaleFactor, yDistance / _canvas.scaleFactor);
        _lineRenderer.SetVerticesDirty();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = GetEventSystemRaycastResults();
        for (int index = 0; index < raycastResults.Count; index++)
        {
            RaycastResult curRaysastResult = raycastResults[index];
            if (curRaysastResult.gameObject.GetComponent<CodeblockReceiver>())
            {
                Attach(curRaysastResult.gameObject.GetComponent<CodeblockReceiver>());

                break;

            } else
            {
                Detach();
            }       
        }
    }

    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Detach();
    }
}
