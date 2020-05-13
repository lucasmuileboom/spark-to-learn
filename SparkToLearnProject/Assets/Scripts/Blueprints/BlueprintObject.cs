using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;

    private Blueprint _blueprint;

    private void Start()
    {
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, GameObject.FindGameObjectWithTag("BlueprintCanvas").transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        _blueprint = blueprintInstance.GetComponent<Blueprint>();
    }

    private void OnMouseDown()
    {
        _blueprint.Show();
    }
}
