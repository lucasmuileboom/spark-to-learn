using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;
    private CursorManager _cursorManager;
    [SerializeField] private Renderer _shaderRenderen;
    [SerializeField] private Renderer _environmentRenderen;

    [SerializeField] private bool isProp;
    [SerializeField] private bool isEnvironment;

    public Blueprint Blueprint { get; private set; }

    private void Start()
    {
        // Instantiate and setup the blueprint
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        Blueprint = blueprintInstance.GetComponent<Blueprint>();
        Blueprint.Hide();
        Blueprint._shaderRenderen = _shaderRenderen;
        Blueprint._environmentRenderen = _environmentRenderen;
        Blueprint._isEnvironment = isEnvironment;
        Blueprint._isProp = isProp;

        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
    }

    private void OnMouseDown()
    {
        // Show this object's blueprint when clicked
        Blueprint.Show();

        _cursorManager.toggleCursor(true);
    }
}
