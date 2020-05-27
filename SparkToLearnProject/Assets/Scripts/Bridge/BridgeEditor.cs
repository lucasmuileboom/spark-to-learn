using UnityEngine;
using UnityEngine.UI;

public class BridgeEditor : MonoBehaviour
{
    [HideInInspector]
    public BridgeBuilder BridgeBuilder;

    [SerializeField]
    private Slider[] _colorSliders;

    [SerializeField]
    private Dropdown _typeDropdown;

    private void Start()
    {
        _typeDropdown.onValueChanged.AddListener(SetType);
        _typeDropdown.value = BridgeBuilder.StartType;

        foreach (Slider s in _colorSliders)
        {
            s.onValueChanged.AddListener(SetColor);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    private void SetType(int value)
    {
        BridgeBuilder.SetType(value);
        SetColor(0);
    }

    private void SetColor(float value)
    {
        float[] vals = { _colorSliders[0].value, _colorSliders[1].value, _colorSliders[2].value };

        BridgeBuilder.SetColor(vals);
    }
}
