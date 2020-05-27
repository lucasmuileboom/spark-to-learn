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

    [SerializeField]
    private Transform _codeblockParent;

    private ZoomUI _zoomUI;

    private CanvasGroup _canvasGroup;

    public Renderer shaderRenderen;
    public Renderer environmentRenderen;
    public bool isProp;
    public bool isEnvironment;
    public bool useLeavesMat;
    public Renderer leavesRenderen;

    [SerializeField]
    private GameObject _colorBlock;

    private void Awake()
    {
        _zoomUI = GetComponent<ZoomUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (_colorBlock != null)
        {
            _zoomUI.AddZoomable(_colorBlock.transform);
            _colorBlock.GetComponent<SetColorShader>().SetUpColorChanger(shaderRenderen, isEnvironment, isProp, useLeavesMat, environmentRenderen, leavesRenderen);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
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
        codeblockInstance.transform.SetParent(_codeblockParent);
        codeblockInstance.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        codeblockInstance.transform.localScale = new Vector3(.75f, .75f, 1);

        _zoomUI.AddZoomable(codeblockInstance.transform);

        codeblockInstance.GetComponent<SetColorShader>().SetUpColorChanger(shaderRenderen, isEnvironment, isProp, useLeavesMat , environmentRenderen , leavesRenderen);
    }

    /// <summary>
    /// Create a when codeblock
    /// </summary>
    public void InstantiateWhenCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_whenCodeblockPrefab);
        codeblockInstance.transform.SetParent(_codeblockParent);
        codeblockInstance.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        codeblockInstance.transform.localScale = new Vector3(.75f, .75f, 1);

        _zoomUI.AddZoomable(codeblockInstance.transform);
    }

    /// <summary>
    /// Start the blueprint
    /// </summary>
    public void ExecuteBlueprint()
    {
        _startCodeblock.ExecuteCodeblock();
    }
}
