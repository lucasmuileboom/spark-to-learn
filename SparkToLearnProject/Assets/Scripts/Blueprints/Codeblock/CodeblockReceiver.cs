using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CodeblockReceiver : MonoBehaviour, IPointerClickHandler
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
        attacher = null;
        OnDetach.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (attacher != null)
        {
            attacher.Detach();
        }
    }
}
