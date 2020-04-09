using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxXRotation;

    public void AddRotationToObject(Vector3 _rotation) 
    {
        Vector3 _newRotation = transform.eulerAngles + _rotation;

        if(transform.eulerAngles.x + _rotation.x >= _maxXRotation && transform.eulerAngles.x + _rotation.x <= 180) 
        {
            _newRotation.x = _maxXRotation;
        }
        else if(transform.eulerAngles.x + _rotation.x <= 360 - _maxXRotation && transform.eulerAngles.x + _rotation.x >= 180) 
        {
            _newRotation.x = -_maxXRotation;
        }

        transform.eulerAngles = _newRotation;
    }
}
