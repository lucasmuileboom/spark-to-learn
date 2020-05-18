using UnityEngine;

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

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Show and hide the blueprints using canvas group so code within can still run
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
    /// Create an event codeblock
    /// </summary>
    public void InstantiateCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_codeblockPrefab);
        codeblockInstance.transform.SetParent(transform);
        codeblockInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Create a when codeblock
    /// </summary>
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
