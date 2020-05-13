using UnityEngine;
using UnityEngine.UI;

public class CodeblockInput : MonoBehaviour
{
    public enum InputTypes { Int, Float, Vector3, String, KeyCode, Color, Audio }

    private CodeblockEvents _events;
    private CodeblockWhens _whens;

    [Header("Event UI")]
    [SerializeField]
    private Dropdown _dropdown;

    [Header("Vector UI")]
    [SerializeField]
    private GameObject _vectorInput;
    [SerializeField]
    private InputField _xInput;
    [SerializeField]
    private InputField _yInput;
    [SerializeField]
    private InputField _zInput;

    [Header("KeyCode UI")]
    [SerializeField]
    private GameObject _keyCodeInput;
    [SerializeField]
    private InputButton _inputButton;

    [Header("Color UI")]
    [SerializeField]
    private GameObject _colorInput;
    [SerializeField]
    private Slider _colorSlider;

    [Header("Audio UI")]
    [SerializeField]
    private GameObject _audioInput;
    [SerializeField]
    private Dropdown _clipDropdown;

    private void Start()
    {
        if (GetComponent<CodeblockEvents>())
        {
            _events = GetComponent<CodeblockEvents>();

            // Fill the event dropdown with available events
            for (int i = 0; i < _events.Events.Length; i++)
            {
                _dropdown.options.Add(new Dropdown.OptionData() { text = _events.Events[i].Name });
            }

            SetEvent();
        }

        if (GetComponent<CodeblockWhens>())
        {
            _whens = GetComponent<CodeblockWhens>();

            // Fill the event dropdown with available events
            for (int i = 0; i < _whens.Whens.Count; i++)
            {
                _dropdown.options.Add(new Dropdown.OptionData() { text = _whens.Whens[i].Name });
            }

            SetWhens();
        }
    }

    /// <summary>
    /// Set the required input method active and disable others for events
    /// </summary>
    public void SetEvent()
    {
        _vectorInput.SetActive(false);
        _colorInput.SetActive(false);
        _keyCodeInput.SetActive(false);
        _audioInput.SetActive(false);

        switch (_events.Events[_dropdown.value].EventParameter)
        {
            case InputTypes.Vector3:
                _vectorInput.SetActive(true);
                break;
            case InputTypes.Color:
                _colorInput.SetActive(true);
                break;
            case InputTypes.KeyCode:
                _keyCodeInput.SetActive(true);
                break;
            case InputTypes.Audio:
                _audioInput.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Set the required input method active and disable others for whens
    /// </summary>
    public void SetWhens()
    {
        _vectorInput.SetActive(false);
        _colorInput.SetActive(false);
        _keyCodeInput.SetActive(false);
        _audioInput.SetActive(false);

        switch (_whens.Whens[_dropdown.value].InputType)
        {
            case InputTypes.Vector3:
                _vectorInput.SetActive(true);
                break;
            case InputTypes.Color:
                _colorInput.SetActive(true);
                break;
            case InputTypes.KeyCode:
                _keyCodeInput.SetActive(true);
                break;
            case InputTypes.Audio:
                _audioInput.SetActive(true);
                break;
        }
    }


    public int GetDropdown()
    {
        return _dropdown.value;
    }

    public Vector3 GetVector()
    {
        return new Vector3(float.Parse(_xInput.text), float.Parse(_yInput.text), float.Parse(_zInput.text));
    }

    public KeyCode GetKeyCode()
    {
        return (_inputButton.SelectedKeyCode);
    }

    public float GetColor()
    {
        return _colorSlider.value;
    }

    public int GetClip()
    {
        return _clipDropdown.value;
    }
}
