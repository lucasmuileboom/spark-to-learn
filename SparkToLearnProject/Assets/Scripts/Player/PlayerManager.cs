using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private MoveObject _moveObject;
    private RotateObject _rotateObject;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _moveObject = GetComponent<MoveObject>();
        _rotateObject = GetComponent<RotateObject>();
    }
    private void Update() 
    {
        PlayerVelocity();
        PlayerRotation();
    }
    private void PlayerVelocity() 
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
    private void PlayerRotation() 
    {
        Vector3 _playerRotation = new Vector3();
        
        if(_inputManager._cameraRotateUpButtonDown()) 
        {
            _playerRotation.x -= 1;
        }
        if(_inputManager._cameraRotateDownButtonDown()) 
        {
            _playerRotation.x += 1;
        }
        if(_inputManager._cameraRotateRightButtonDown()) 
        {
            _playerRotation.y += 1;
        }
        if(_inputManager._cameraRotateLeftButtonDown()) 
        {
            _playerRotation.y -= 1;
        }
        _playerRotation.Normalize();
        _rotateObject.AddRotationToObject(_playerRotation);
    }
}
