using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectScript : MonoBehaviour
{
    [SerializeField] private KeyCode _testKey = KeyCode.Space;
    [SerializeField] private GameObject _testObject;
    [SerializeField] private LayerMask _mask;

    private bool _readyToPlace = true;
    private bool _activateSpawn = false;
    private GameObject _currentObject;


    // Update is called once per frame
    void Update()
    {
        _activateSpawn = Input.GetKeyDown(_testKey);
        if (_activateSpawn && _readyToPlace)
        {
            Debug.Log("Highlight");
            _readyToPlace = false;
            _currentObject = _testObject;
            StartCoroutine(SpawnObject.HighlightObjectOnRaycastHit(gameObject, _currentObject, BreakConditionPreview, _mask));
        }
    }

    //Condition for exitting the preview of the object
    private bool BreakConditionPreview(GameObject instance)
    {
        if (_activateSpawn)
        {
            Debug.Log("BreakPreview");
            StartCoroutine(SpawnObject.RotateObject(instance, KeyCode.LeftArrow, KeyCode.RightArrow, 0.5f, BreakConditionRotating));
            return true;
        }
        else return false;
    }

    //Condition for exitting when rotating the object
    private bool BreakConditionRotating(GameObject rotatedObject)
    {
        if (_activateSpawn)
        {
            Debug.Log("BreakRotating");
            SpawnObject.Instantiate(rotatedObject);
            _readyToPlace = true;
            return true;
        }
        else return false;
    }
}
