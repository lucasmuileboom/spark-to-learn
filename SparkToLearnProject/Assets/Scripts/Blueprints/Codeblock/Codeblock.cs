using UnityEngine;

public class Codeblock : MonoBehaviour
{
    private CodeblockEvents _events;
    private CodeblockInput _input;

    [SerializeField]
    private CodeblockAttacher _attacher;
    [SerializeField]
    private CodeblockReceiver _receiver;

    private void Start()
    {
        _events = GetComponent<CodeblockEvents>();
        _input = GetComponent<CodeblockInput>();
    }

    /// <summary>
    /// Run the selected event
    /// </summary>
    public void Execute()
    {
        _events.Events[_input.GetDropdown()].Event.Invoke();

        // Execute next codeblock if one is attached
        if (_attacher.attachedReceiver)
        {
            _attacher.attachedReceiver.codeblock.Execute();
        }
    }

    /// <summary>
    /// Detach both ends of this codeblock
    /// </summary>
    public void Detach()
    {
        _attacher.Detach();
        if (_receiver.attacher)
        {
            _receiver.attacher.Detach();
        }
    }

    /// <summary>
    /// Detach and remove this codeblock
    /// </summary>
    public void Remove()
    {
        Detach();

        Destroy(gameObject);
    }
}
