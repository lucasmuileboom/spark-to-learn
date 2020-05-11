using UnityEngine;
using UnityEngine.UI;

public class StartCodeblock : MonoBehaviour
{
    [SerializeField]
    private CodeblockAttacher _attacher;

    [SerializeField]
    private Toggle repeatToggle;

    private void Update()
    {
        if (repeatToggle.isOn)
        {
            ExecuteCodeblock();
        }
    }

    public void ExecuteCodeblock()
    {
        if (_attacher.attachedReceiver)
        {
            _attacher.attachedReceiver.codeblock.Execute();
        }
    }
}
