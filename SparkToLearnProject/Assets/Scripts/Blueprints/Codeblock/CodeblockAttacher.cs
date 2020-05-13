using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeblockAttacher : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
            Vector2 linePosition = new Vector2(attachedReceiver.gameObject.transform.position.x - transform.position.x + ((attachedReceiver.gameObject.transform.position.x - transform.position.x) / 6), attachedReceiver.gameObject.transform.position.y - transform.position.y + ((attachedReceiver.gameObject.transform.position.y - transform.position.y) / 6));

            _lineRenderer.Points[1] = linePosition;

            _lineRenderer.SetVerticesDirty();
        }
    }

    public void Attach(CodeblockReceiver receiver)
    {
        attachedReceiver = receiver;
        receiver.SetAttacher(this);
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
        _lineRenderer.Points[1] += eventData.delta / _canvas.scaleFactor;
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
}
