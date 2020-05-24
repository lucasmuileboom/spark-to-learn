using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{
    [SerializeField]
    private Text _inputText;

    private bool _waitingForInput = false;

    public KeyCode SelectedKeyCode = KeyCode.A;

    public void WaitForInput()
    {
        _waitingForInput = true;
    }

    private void Update()
    {
        if (_waitingForInput)
        {
            _inputText.text = "Press any key";

            // Get the next key pressed
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    SelectedKeyCode = vKey;
                    _inputText.text = vKey.ToString();
                    _waitingForInput = false;
                }
            }
        }
    }
}
