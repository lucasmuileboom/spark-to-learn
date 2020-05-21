using UnityEngine;
using UnityEngine.Events;

public class CodeblockReceiver : MonoBehaviour
{
    public Codeblock codeblock;

    public CodeblockAttacher attacher;

    public UnityEvent OnReceive;
    public UnityEvent OnDetach;

    public bool AllowMultipleReceivers;

    public void SetAttacher(CodeblockAttacher attacher)
    {
        this.attacher = attacher;

        OnReceive.Invoke();
    }

    public void RemoveAttacher()
    {
        this.attacher = null;

        OnDetach.Invoke();
    }
}
