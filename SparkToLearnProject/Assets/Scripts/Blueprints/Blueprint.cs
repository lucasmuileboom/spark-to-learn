using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public GameObject blueprintObject;

    [SerializeField]
    private StartCodeblock _startBlock;

    [SerializeField]
    private GameObject _codeblockPrefab;

    public void InstantiateCodeblock()
    {
        GameObject codeblockInstance = Instantiate(_codeblockPrefab);
        codeblockInstance.transform.SetParent(transform);
        codeblockInstance.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ExecuteBlueprint()
    {
        _startBlock.ExecuteCodeblock(blueprintObject);
    }
}
