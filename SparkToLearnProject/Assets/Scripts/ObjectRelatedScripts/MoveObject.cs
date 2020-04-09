using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void GiveRigidbodyVelocity(Vector3 velocity) 
    {
        _rigidbody.velocity = velocity * _movementSpeed;
    }
}
