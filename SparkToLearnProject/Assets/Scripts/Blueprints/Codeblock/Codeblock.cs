using UnityEngine;
using UnityEngine.UI;

public class Codeblock : MonoBehaviour
{
    [SerializeField]
    private Dropdown _eventDropdown;

    private CodeblockEvents _codeblockEvents;

    [SerializeField]
    private CodeblockAttacher _attacher;
    [SerializeField]
    private CodeblockReceiver _receiver;

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
        _codeblockEvents = GetComponent<CodeblockEvents>();

        for(int i = 0; i < _codeblockEvents.events.Length; i++)
        {
            _eventDropdown.options.Add(new Dropdown.OptionData() { text = _codeblockEvents.events[i].Name });
        }

        SetCodeblockEvent();
    }

    public void ExecuteCodeblock(GameObject obj)
    {
        _codeblockEvents.Object = obj;
        _codeblockEvents.events[_eventDropdown.value].Event.Invoke();

        if (_attacher.attachedReceiver)
        {
            _attacher.attachedReceiver.codeblock.ExecuteCodeblock(obj);
        }
    }

    /// <summary>
    /// Get the input for vectors
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetVectorInput()
    {
        return new Vector3(float.Parse(_xInput.text), float.Parse(_yInput.text), float.Parse(_zInput.text));
    }

    /// <summary>
    /// Get the input for colors
    /// </summary>
    /// <returns>Color</returns>
    public float ColorInput()
    {
        return _colorSlider.value;
    }

    public void SetCodeblockEvent()
    {
        _vectorInput.SetActive(false);
        _colorInput.SetActive(false);

        switch (_codeblockEvents.events[_eventDropdown.value].EventParameter)
        {
            case CodeblockEvents.EventParameters.Vector3:
                _vectorInput.SetActive(true);
                break;
            case CodeblockEvents.EventParameters.Color:
                _colorInput.SetActive(true);
                break;
        }
    }

    public void RemoveCodeBlock()
    {
        _attacher.Detach();
        if (_receiver.attacher)
        {
            _receiver.attacher.Detach();
        }

        Destroy(gameObject);
    }
}
