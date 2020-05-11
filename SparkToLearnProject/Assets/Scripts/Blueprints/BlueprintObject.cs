using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;

    private GameObject _blueprint;

    private void Start()
    {
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.SetActive(false);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        _blueprint = blueprintInstance;
    }

    private void OnMouseDown()
    {
        _blueprint.SetActive(true);
    }
}
