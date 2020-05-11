using UnityEngine;
using UnityEngine.UI;

public class CodeblockInput : MonoBehaviour
{
    private CodeblockEvents _events;

    [Header("Event UI")]
    [SerializeField]
    private Dropdown _eventDropdown;

    [Header("Vector UI")]
    [SerializeField]
    private GameObject _vectorInput;
    [SerializeField]
    private InputField _xInput;
    [SerializeField]
    private InputField _yInput;
    [SerializeField]
    private InputField _zInput;

    [Header("Color UI")]
    [SerializeField]
    private GameObject _colorInput;
    [SerializeField]
    private Slider _colorSlider;

    private void Start()
    {
        _events = GetComponent<CodeblockEvents>();

        // Fill the event dropdown with available events
        for (int i = 0; i < _events.Events.Length; i++)
        {
            _eventDropdown.options.Add(new Dropdown.OptionData() { text = _events.Events[i].Name });
        }

        SetEvent();
    }

    /// <summary>
    /// Set the required input method active and disable others
    /// </summary>
    public void SetEvent()
    {
        _vectorInput.SetActive(false);
        _colorInput.SetActive(false);

        switch (_events.Events[_eventDropdown.value].EventParameter)
        {
            case CodeblockEvents.EventParameters.Vector3:
                _vectorInput.SetActive(true);
                break;
            case CodeblockEvents.EventParameters.Color:
                _colorInput.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Get which event is selected
    /// </summary>
    /// <returns>Int</returns>
    public int GetEvent()
    {
        return _eventDropdown.value;
    }

    /// <summary>
    /// Get the input for vectors
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetVector()
    {
        return new Vector3(float.Parse(_xInput.text), float.Parse(_yInput.text), float.Parse(_zInput.text));
    }

    /// <summary>
    /// Get the input for colors
    /// </summary>
    /// <returns>Color</returns>
    public float GetColor()
    {
        return _colorSlider.value;
    }
}
