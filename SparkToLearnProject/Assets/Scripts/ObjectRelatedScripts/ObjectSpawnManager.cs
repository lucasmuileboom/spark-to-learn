using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private ItemListCycle _spawnableItems;
    [SerializeField] private SceneManager _sceneManager;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _camera;

    private GameObject _currentObject;
    private bool _placingProcess = false;
    private bool _placeActive = false;

    void Update()
    {
        _placeActive = _inputManager.SpawnObjectButtonPress();
        if (!_placingProcess && _placeActive)
        {
            _currentObject = _spawnableItems.GetItem().ObjectReference;
            _placingProcess = true;
            StartCoroutine(ObjectSpawner.HighlightObjectOnRaycastHit(_camera, _currentObject, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, 0.5f, BreakCondition, _layerMask));
        }
    }

    private bool BreakCondition(GameObject rotatedObject)
    {
        if (_placeActive)
        {
            ItemDetails details = rotatedObject.GetComponent<ItemDetails>();
            details.InstantiateSelf(rotatedObject.transform);
            _sceneManager.AddObject(details);
            _playerManager.enabled = true;
            _placingProcess = false;
            return true;
        }
        else return false;
    }
}
