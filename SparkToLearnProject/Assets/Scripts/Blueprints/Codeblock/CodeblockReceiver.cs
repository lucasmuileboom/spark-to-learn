using UnityEngine;

public class CodeblockReceiver : MonoBehaviour
{
    public Codeblock codeblock;

    public CodeblockAttacher attacher;

    public void SetAttacher(CodeblockAttacher attacher)
    {
        this.attacher = attacher;
    }

    public void RemoveAttacher()
    {
        this.attacher = null;
    }
}
