using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _MaxXRotation;

    public void AddRotationToObject(Vector3 _rotation) 
    {
        Vector3 _newRotation = transform.eulerAngles + _rotation;

        if(transform.eulerAngles.x + _rotation.x >= _MaxXRotation && transform.eulerAngles.x + _rotation.x <= 180) 
        {
            _newRotation.x = _MaxXRotation;
        }
        else if(transform.eulerAngles.x + _rotation.x <= 360 - _MaxXRotation && transform.eulerAngles.x + _rotation.x >= 180) 
        {
            _newRotation.x = -_MaxXRotation;
        }

        transform.eulerAngles = _newRotation;
    }
}
