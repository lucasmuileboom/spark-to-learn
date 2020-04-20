using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private ItemListCycle _spawnableItems;

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
            StartCoroutine(ObjectSpawner.HighlightObjectOnRaycastHit(_camera, _currentObject, BreakConditionPreview, _layerMask));
        }
    }

    private bool BreakConditionPreview(GameObject spawnedObject)
    {
        if (_placeActive)
        {
            if (_playerManager != null)
            {
                _playerManager.enabled = false;
            }
            StartCoroutine(ObjectSpawner.RotateObject(spawnedObject, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, 0.5f, BreakConditionRotating));
            return true;
        }
        else return false;
    }

    private bool BreakConditionRotating(GameObject rotatedObject)
    {
        if (_placeActive)
        {
            ObjectSpawner.Instantiate(rotatedObject);
            _playerManager.enabled = true;
            _placingProcess = false;
            return true;
        }
        else return false;
    }
}
