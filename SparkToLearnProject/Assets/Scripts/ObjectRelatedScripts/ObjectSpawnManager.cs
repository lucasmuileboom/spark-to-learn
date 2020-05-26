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
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _camera;

    private GameObject _currentObject;
    private List<Material> _originalMat;
    private bool _placingProcess = false;
    private bool _placeActive = false;
    void Update()
    {
        _placeActive = _inputManager.SpawnObjectButtonPress();
        if (!_placingProcess && _placeActive)
        {
            _originalMat = new List<Material>();
            _currentObject = _spawnableItems.GetItem().ObjectReference;
            _placingProcess = true;
            foreach (MeshRenderer mesh in _spawnableItems.GetItem().Mesh)
            {
                Material mat = new Material(mesh.sharedMaterial);
                _originalMat.Add((mat == null)?new Material(_defaultMaterial):mat);
            }
            StartCoroutine(ObjectSpawner.HighlightObjectOnRaycastHit(_camera, _currentObject, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, 0.5f, BreakConditionSpawn, _layerMask, _correctHighlight, _incorrectHighlight));
        }
    }

    public void MoveObject(ItemDetails item)
    {
        if (!_placingProcess)
        {
            _originalMat = new List<Material>();
            _playerManager.ActivateSkill2();
            _placingProcess = true;
            foreach (MeshRenderer mesh in item.Mesh)
            {
                Debug.Log(mesh.material);
                Material mat = new Material(mesh.sharedMaterial);
                _originalMat.Add((mat == null)?new Material(_defaultMaterial):mat);
            }
            StartCoroutine(ObjectSpawner.RepositionObject(_camera, item.gameObject, _inputManager.RotateObjectLeftButtonDown, _inputManager.RotateObjectRightButtonDown, 0.5f, BreakConditionMove, _layerMask, _correctHighlight, _incorrectHighlight));
        }
    }

    private bool BreakConditionMove(GameObject movedObject)
    {
        if (_placeActive)
        {
            ItemDetails details = movedObject.GetComponent<ItemDetails>();
            foreach (Collider _col in details.Colliders)
            {
                _col.isTrigger = false;
            }
            for (int i = 0; i < details.Mesh.Count; i++)
            {
                Debug.Log(_originalMat[i].name);
                details.Mesh[i].material = _originalMat[i];
            }
            _playerManager.enabled = true;
            _placingProcess = false;
            _originalMat = null;
            return true;
        }
        else return false;
    }

    private bool BreakConditionSpawn(GameObject placedObject)
    {
        if (_placeActive)
        {
            ItemDetails details = placedObject.GetComponent<ItemDetails>();
            foreach(Collider _col in details.Colliders)
            {
                _col.isTrigger = false;
            }
            for (int i = 0; i < details.Mesh.Count; i++)
            {
                details.Mesh[i].material = _originalMat[i];
            }
            details.SetInstance(placedObject);
            _sceneManager.AddObject(details);
            _playerManager.enabled = true;
            _placingProcess = false;
            _originalMat = null;
            return true;
        }
        else return false;
    }
}
