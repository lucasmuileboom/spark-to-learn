using UnityEngine;

public class WhenCodeblock : MonoBehaviour
{
    [SerializeField]
    private CodeblockAttacher _attacher;

    public void Execute()
    {
        // Execute next codeblock if one is attached
        if (_attacher.attachedReceiver)
        {
            _attacher.attachedReceiver.codeblock.Execute();
        }
    }

    /// <summary>
    /// Detach this codeblock
    /// </summary>
    public void Detach()
    {
        _attacher.Detach();
    }

    /// <summary>
    /// Detach and remove this codeblock
    /// </summary>
    public void Remove()
    {
        Detach();

        transform.parent.parent.GetComponent<ZoomUI>().RemoveZoomable(transform);

        Destroy(gameObject);
    }
}
