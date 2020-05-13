using UnityEngine;
using UnityEngine.UI;

public class Blueprint : MonoBehaviour
{
    [HideInInspector]
    public GameObject Object;

    [SerializeField]
    private StartCodeblock _startCodeblock;

    [SerializeField]
    private GameObject _codeblockPrefab;
    [SerializeField]
    private GameObject _whenCodeblockPrefab;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Create a codeblock
    /// </summary>
    public void InstantiateCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_codeblockPrefab);
        codeblockInstance.transform.SetParent(transform);
        codeblockInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    public void InstantiateWhenCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_whenCodeblockPrefab);
        codeblockInstance.transform.SetParent(transform);
        codeblockInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Start the blueprint
    /// </summary>
    public void ExecuteBlueprint()
    {
        _startCodeblock.ExecuteCodeblock();
    }
}
