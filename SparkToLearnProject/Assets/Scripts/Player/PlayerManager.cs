using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private MoveObject _moveObject;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _moveObject = GetComponent<MoveObject>();
    }
    private void Update() 
    {
        CheckPlayerVelocity();
    }
    private void CheckPlayerVelocity() 
    {
        Vector3 _playerVelocity = new Vector3();

        if(_inputManager._moveForwardButtonDown()) 
        {
            _playerVelocity += this.transform.forward;
        }
        if(_inputManager._moveBackwardButtonDown()) 
        {
            _playerVelocity -= this.transform.forward;
        }
        if(_inputManager._moveRightButtonDown()) 
        {
            _playerVelocity += this.transform.right;
        }
        if(_inputManager._moveLeftButtonDown()) 
        {
            _playerVelocity -= this.transform.right;
        }
        _playerVelocity.Normalize();
        _moveObject.GiveRigidbodyVelocity(_playerVelocity);
    }
}
