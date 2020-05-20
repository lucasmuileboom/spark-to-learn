using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorShader : MonoBehaviour
{
    private Renderer _meshRenderer;
    private Material _material;
    private Color _startColor1;
    private Color _startColor2;
    private Color _startColor3;

    public void SetUpColorChanger(Renderer ShaderRenderer)
    {
        _meshRenderer = ShaderRenderer;
        _material = _meshRenderer.sharedMaterial;
        _startColor1 = _material.GetColor("_Mask1_color");
        _startColor2 = _material.GetColor("_Mask2_color");
        _startColor3 = _material.GetColor("_Mask3_color");
    }

    private void Update()
    {
       // SetColor(Color.red, Color.blue, Color.green);
    }
    public Color GetColor(int i)
    {
        if (i == 0) 
        {
            return _material.GetColor("_Mask1_color");
        }
        else if (i == 1)
        {
            return _material.GetColor("_Mask2_color");
        }
        else if (i == 2)
        {
            return _material.GetColor("_Mask3_color");
        }
        return Color.white;
    }
    public void SetColor(Color newColor1 , Color newColor2 , Color newColor3) 
    {
        _material.SetColor("_Mask1_color", newColor1);        
        _material.SetColor("_Mask2_color", newColor2);
        _material.SetColor("_Mask3_color", newColor3);
    }

    private void OnDestroy()
    {
        SetColor(_startColor1, _startColor2, _startColor3);
    }
}
