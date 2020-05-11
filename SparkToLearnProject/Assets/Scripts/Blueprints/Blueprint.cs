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
