using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private ItemListCycle _spawnableItems;
    [SerializeField] private SceneManager _sceneManager;

    [SerializeField] private Material _correctHighlight;
    [SerializeField] private Material _incorrectHighlight;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _camera;

    private GameObject _currentObject;
    private MeshRenderer _originalMat;
    private bool _placingProcess = false;
    private bool _placeActive = false;
    void Update()
    {
        _placeActive = _inputManager.SpawnObjectButtonPress();
        if (!_placingProcess && _placeActive)
        {
            _currentObject = _spawnableItems.GetItem().ObjectReference;
            _placingProcess = true;
            _originalMat = _currentObject.GetComponent<MeshRenderer>();
            StartCoroutine(ObjectSpawner.HighlightObjectOnRaycastHit(_camera, _currentObject, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, _rotateSpeed, BreakConditionSpawn, _layerMask, _correctHighlight, _incorrectHighlight));
        }
    }

    public void MoveObject(ItemDetails item)
    {
        if (!_placingProcess)
        {
            _placingProcess = true;
            StartCoroutine(ObjectSpawner.RepositionObject(_camera, item.Instance, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, 0.5f, BreakConditionMove, _layerMask, _correctHighlight, _incorrectHighlight));
        }
    }

    private bool BreakConditionMove(GameObject movedObject)
    {
        if (_placeActive)
        {
            movedObject.GetComponent<Collider>().isTrigger = false;
            movedObject.GetComponent<MeshRenderer>().material = _originalMat.sharedMaterial;
            _playerManager.enabled = true;
            _placingProcess = false;
            return true;
        }
        else return false;
    }

    private bool BreakConditionSpawn(GameObject rotatedObject)
    {
        if (_placeActive)
        {
            ItemDetails details = rotatedObject.GetComponent<ItemDetails>();
            rotatedObject.GetComponent<Collider>().isTrigger = false;
            rotatedObject.GetComponent<MeshRenderer>().material = _originalMat.sharedMaterial;
            details.SetInstance(rotatedObject);
            _sceneManager.AddObject(details);
            _playerManager.enabled = true;
            _placingProcess = false;
            return true;
        }
        else return false;
    }
}
