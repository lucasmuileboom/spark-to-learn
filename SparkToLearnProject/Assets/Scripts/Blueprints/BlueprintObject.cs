using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlueprintObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _blueprintPrefab;
    private CursorManager _cursorManager;
    private PlayerManager _playerManager;

    [SerializeField] private bool _canBeClicked;

    [SerializeField] private Renderer _shaderRenderen;
    [SerializeField] private Renderer _environmentRenderen;
    [SerializeField] private Renderer _leavesRenderen;

    [SerializeField] private bool _isProp;
    [SerializeField] private bool _isEnvironment;
    [SerializeField] private bool _useLeavesMat;

    public Blueprint Blueprint { get; private set; }

    private void Start()
    {
        // Instantiate and setup the blueprint
        GameObject blueprintInstance = Instantiate(_blueprintPrefab, FindObjectOfType<Canvas>().transform);
        blueprintInstance.name = "Blueprint - " + gameObject.name;
        blueprintInstance.GetComponent<Blueprint>().Object = gameObject;

        Blueprint = blueprintInstance.GetComponent<Blueprint>();
        Blueprint.Hide();
        Blueprint.shaderRenderen = _shaderRenderen;
        Blueprint.environmentRenderen = _environmentRenderen;
        Blueprint.isEnvironment = _isEnvironment;
        Blueprint.isProp = _isProp;
        Blueprint.useLeavesMat = _useLeavesMat;
        Blueprint.leavesRenderen = _leavesRenderen;

        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnMouseDown()
    {

        // Show this object's blueprint when clicked
        if (_canBeClicked && _playerManager.canEdit && !EventSystem.current.IsPointerOverGameObject())
        {
            Blueprint.Show();

            _cursorManager.toggleCursor(true);
        }
    }
}
