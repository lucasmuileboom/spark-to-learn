using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ZoomUI : MonoBehaviour
{
    [SerializeField]
    private float[] _scaleFactors = { 0.5f, 0.75f, 1f };
    [HideInInspector]
    public int CurrentScaleFactor;

    [Serializable]
    public struct Zoomable
    {
        public Transform transform;
        public Vector3 initialScale;

        public Zoomable(Transform transform, Vector3 initialScale)
        {
            this.transform = transform;
            this.initialScale = initialScale;
        }
    }

    [SerializeField]
    private List<Zoomable> _zoomables;

    [HideInInspector]
    public UnityEvent OnScale;

    private void Awake()
    {
        CurrentScaleFactor = _scaleFactors.Length - 1;
    }

    public void AddZoomable(Transform newObject)
    {
        bool contains = _zoomables.Any(x => x.transform == newObject);

        if (contains)
        {
            Debug.Log("Zoomable objects already contains " + newObject.name);
            return;
        }

        _zoomables.Add(new Zoomable(newObject, newObject.localScale));

        ScaleZoomables();
    }

    public void RemoveZoomable(Transform removeObject)
    {
        _zoomables.RemoveAll(x => x.transform == removeObject);
    }

    public void ZoomIn()
    {
        if (CurrentScaleFactor >= _scaleFactors.Length - 1)
            return;

        CurrentScaleFactor++;

        ScaleZoomables();
    }

    public void ZoomOut()
    {
        if (CurrentScaleFactor <= 0)
            return;

        CurrentScaleFactor--;

        ScaleZoomables();
    }

    private void ScaleZoomables()
    {
        foreach (Zoomable obj in _zoomables)
        {
            obj.transform.localScale = obj.initialScale * _scaleFactors[CurrentScaleFactor];

            if (obj.transform.GetComponentsInChildren<UILineRenderer>().Length > 0)
            {
                AdjustLineRenderer(obj.transform);
            }
        }

        OnScale.Invoke();
    }

    /// <summary>
    /// Unparent, scale and reparent so that line distances don't mess up
    /// </summary>
    /// <param name="t"></param>
    private void AdjustLineRenderer(Transform t)
    {
        foreach(UILineRenderer lineRenderer in t.GetComponentsInChildren<UILineRenderer>())
        {
            Transform parent = lineRenderer.transform.parent;

            lineRenderer.transform.SetParent(FindObjectOfType<Canvas>().transform);

            lineRenderer.transform.localScale = Vector3.one;

            lineRenderer.transform.SetParent(parent);
        }
    }
}
