using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;

    public Blueprint Blueprint { get; private set; }

    private void Start()
    {
        // Instantiate and setup the blueprint
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        Blueprint = blueprintInstance.GetComponent<Blueprint>();
        Blueprint.Hide();
    }

    private void OnMouseDown()
    {
        // Show this object's blueprint when clicked
        Blueprint.Show();
    }
}
