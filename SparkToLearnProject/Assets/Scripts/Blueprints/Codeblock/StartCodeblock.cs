using UnityEngine;

public class StartCodeblock : MonoBehaviour
{
    [SerializeField]
    private CodeblockAttacher _attacher;

    public void ExecuteCodeblock()
    {
        if (_attacher.attachedReceiver)
        {
            _attacher.attachedReceiver.codeblock.Execute();
        }
    }
}
