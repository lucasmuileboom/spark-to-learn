using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectScript : MonoBehaviour
{
    [SerializeField] private KeyCode _testKey = KeyCode.Space;
    [SerializeField] private GameObject _testObject;
    [SerializeField] private LayerMask _mask;

    private bool _readyToPlace = false;
    private bool _activateSpawn = false;


    // Update is called once per frame
    void Update()
    {
        _activateSpawn = Input.GetKeyDown(_testKey);
        if (_activateSpawn && !_readyToPlace)
        {
            Debug.Log("Highlight");
            _readyToPlace = true;
            StartCoroutine(SpawnObject.HighlightObjectOnRaycastHit(gameObject, _testObject,BreakCondition,_mask));
        }
    }
    private bool BreakCondition()
    {
        if (_readyToPlace && _activateSpawn)
        {
            Debug.Log("Break");
            SpawnObject.SpawnObjectOnRaycastHit(transform, _testObject, _mask);
            _readyToPlace = false;
            return true;
        }
        else return false;
    }
}
