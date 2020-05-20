using UnityEngine;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;
    private CursorManager _cursorManager;
    private PlayerManager _playerManager;

    [SerializeField] private bool _canBeClicked;

    public Blueprint Blueprint { get; private set; }

    private void Start()
    {
        // Instantiate and setup the blueprint
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        Blueprint = blueprintInstance.GetComponent<Blueprint>();
        Blueprint.Hide();

        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnMouseDown()
    {
        // Show this object's blueprint when clicked
        if (_canBeClicked && _playerManager._canEdit) 
        {
            Blueprint.Show();

            _cursorManager.toggleCursor(true);
        }
    }
}
