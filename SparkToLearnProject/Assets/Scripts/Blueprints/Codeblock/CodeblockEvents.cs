using System;
using UnityEngine;
using UnityEngine.Events;

public class CodeblockEvents : MonoBehaviour
{
    private Codeblock _codeblock;
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
        Object.transform.position = new Vector3(Object.transform.position.x + _codeblock.GetVectorInput().x
            , Object.transform.position.y + _codeblock.GetVectorInput().y
            , Object.transform.position.z + _codeblock.GetVectorInput().z);
    }

    public void Rotate()
    {
        Object.transform.rotation = Quaternion.Euler(_codeblock.GetVectorInput());
    }

    public void Scale()
    {
        Object.transform.localScale = _codeblock.GetVectorInput();
    }

    public void Color()
    {
        Object.GetComponent<Renderer>().material.color = _codeblock.ColorInput();
    }
}
