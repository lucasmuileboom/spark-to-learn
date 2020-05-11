using System;
using UnityEngine;
using UnityEngine.Events;

public class CodeblockEvents : MonoBehaviour
{
    private Blueprint _blueprint;
    private CodeblockInput _input;

    public enum EventParameters { Int, Float, Vector3, Color }

    [Serializable]
    public struct EventStruct
    {
        public string Name;
        public EventParameters EventParameter;
        public UnityEvent Event;
    }

    [SerializeField]
    public EventStruct[] Events;

    private void Start()
    {
        _blueprint = transform.parent.GetComponent<Blueprint>();
        _input = GetComponent<CodeblockInput>();
    }

    public void Move()
    {
        _blueprint.Object.transform.Translate(_input.GetVector(), Space.Self);
    }

    public void Rotate()
    {
        _blueprint.Object.transform.Rotate(_input.GetVector());
    }

    public void Scale()
    {
        _blueprint.Object.transform.localScale = _input.GetVector();
    }

    public void ChangeColor()
    {
        float h;
        float s;
        float v;
        Color.RGBToHSV(_blueprint.Object.GetComponent<Renderer>().material.color, out h, out s, out v);

        if (s == 0)
        {
            s = 1;
        }

        h = _input.GetColor();

        _blueprint.Object.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
    }
}
