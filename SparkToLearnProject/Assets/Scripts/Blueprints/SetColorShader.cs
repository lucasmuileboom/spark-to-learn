using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorShader : MonoBehaviour
{
    private Renderer _meshRenderer;
    private Material _environmentMaterial;
    private Material _material;
    private Color _startColor1;
    private Color _startColor2;
    private Color _startColor3;
    private bool _canChangeColor = false;
    private bool _isEnvironment = false;
    private bool _isProp = false;

    public void SetUpColorChanger(Renderer shaderRenderer, bool isEnvironment, bool isProp, Renderer environmentRenderen)
    {
        _isEnvironment = isEnvironment;
        _isProp = isProp;

        _meshRenderer = shaderRenderer;

        if (!isProp)
        {
            _material = _meshRenderer.sharedMaterial;
        }
        else 
        {
            _material = _meshRenderer.material;
        }

        if (isEnvironment)
        {
            _environmentMaterial = environmentRenderen.sharedMaterial;
        }
        
        _startColor1 = _material.GetColor("_Mask1_color");
        _startColor2 = _material.GetColor("_Mask2_color");
        _startColor3 = _material.GetColor("_Mask3_color");
        _canChangeColor = true;
    }

    private void Update()
    {
       // SetColor(Color.red, Color.blue, Color.green);
    }
    public Color GetColor(int i)
    {
        if (_canChangeColor)
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
        }
        return Color.white;
    }
    public void SetColor(Color newColor1 , Color newColor2 , Color newColor3) 
    {
        if (_canChangeColor) 
        {
            _material.SetColor("_Mask1_color", newColor1);
            _material.SetColor("_Mask2_color", newColor2);
            _material.SetColor("_Mask3_color", newColor3);
            if (_isEnvironment) 
            {
                _environmentMaterial.SetColor("_Color", newColor1);
            }
        }
       
    }

    private void OnDestroy()
    {
        SetColor(_startColor1, _startColor2, _startColor3);
    }
}
