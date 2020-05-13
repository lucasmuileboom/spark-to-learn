using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;
    private CursorManager _CursorManager;

    private Blueprint _blueprint;

    private void Start()
    {
        // Instantiate and setup the blueprint
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        _blueprint = blueprintInstance.GetComponent<Blueprint>();
        _blueprint.Hide();

        _CursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
    }

    private void OnMouseDown()
    {
        // Show this object's blueprint when clicked
        _blueprint.Show();
        _CursorManager.toggleCursor(true);
    }
}
