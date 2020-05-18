using UnityEngine;

public class StartCodeblock : MonoBehaviour
{
    [SerializeField]
    private CodeblockAttacher _attacher;

    [SerializeField]
    private bool _repeat;

    private void Update()
    {
        if (_repeat)
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
