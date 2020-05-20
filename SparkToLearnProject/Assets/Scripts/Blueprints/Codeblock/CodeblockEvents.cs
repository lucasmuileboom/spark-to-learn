using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SetColorShader))]

public class CodeblockEvents : MonoBehaviour
{
    private Blueprint _blueprint;
    private CodeblockInput _input;
    private SetColorShader _setColorShader;

    [Serializable]
    public struct EventStruct
    {
        public string Name;
        public CodeblockInput.InputTypes EventParameter;
        public UnityEvent Event;
    }

    [SerializeField]
    public EventStruct[] Events;

    private void Start()
    {
        _blueprint = transform.parent.GetComponent<Blueprint>();
        _input = GetComponent<CodeblockInput>();

        _setColorShader = GetComponent<SetColorShader>();
    }

    #region Events
    public void Move()
    {
        _blueprint.Object.transform.Translate(_input.GetVector(), Space.World);
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
        Color[] _Colors;
        _Colors = new Color[3];

        for (int i = 0; i < 3; i++)
        {
            float h;
            float s;
            float v;
            Color.RGBToHSV(_setColorShader.GetColor(i), out h, out s, out v);
            
            if (s == 0)
            {
                s = 1;
            }

            h = _input.GetColor(i);

            _Colors[i] = Color.HSVToRGB(h, s, v);
        }
        _setColorShader.SetColor(_Colors[0], _Colors[1], _Colors[2]);
        //_blueprint.Object.GetComponent<Renderer>().material.color = Color.HSVToRGB(h, s, v);
    }

    public void PlayClip()
    {
        AudioPlayer.PlayClip(_input.GetClip());
    }
    #endregion
}
