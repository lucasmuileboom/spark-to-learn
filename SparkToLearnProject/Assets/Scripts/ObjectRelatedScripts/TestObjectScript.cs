using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectScript : MonoBehaviour
{
    [SerializeField] private KeyCode _testKey = KeyCode.Space;
    [SerializeField] private GameObject _testObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_testKey))
        {
            Debug.Log("hello");
            SpawnObject.SpawnObjectOnRaycastHit(transform, _testObject);
        }
    }
}
