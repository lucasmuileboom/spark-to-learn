using System;
using UnityEngine;
using UnityEngine.Events;

public class CodeblockEvents : MonoBehaviour
{
    private Codeblock _codeblock;
    [HideInInspector]
    public GameObject Object;

    public enum EventParameters { Int, Float, Vector3, Color }

    [Serializable]
    public struct EventStruct
    {
        public string Name;
        public EventParameters EventParameter;
        public UnityEvent Event;
    }

    [SerializeField]
    public EventStruct[] events;

    private void Start()
    {
        _codeblock = GetComponent<Codeblock>();
    }

    public void Move()
    {
        Object.transform.Translate(_codeblock.GetVectorInput(), Space.Self);
    }

    public void Rotate()
    {
        Object.transform.Rotate(_codeblock.GetVectorInput());
    }

    public void Scale()
    {
        Object.transform.localScale = _codeblock.GetVectorInput();
    }

    public void ChangeColor()
    {
        Debug.Log("Doing the thing!");

        float h;
        float s;
        float v;
        Color.RGBToHSV(Object.GetComponent<Renderer>().material.color, out h, out s, out v);

        Debug.Log(_codeblock.ColorInput());

        h = _codeblock.ColorInput();

        Object.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
    }
}
