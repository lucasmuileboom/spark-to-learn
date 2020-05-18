using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float GravityMultiplier;

    private InputManager _inputManager;
    private MoveObject _moveObject;
    private RotateObject _rotateObjectPlayer;
    private RotateObject _rotateObjectcamera;

    private int _layerMask = 1 << 9;
    private bool GravityIsOn = true;


    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _moveObject = GetComponent<MoveObject>();
        _rotateObjectPlayer = GetComponent<RotateObject>();
        _rotateObjectcamera = Camera.main.GetComponent<RotateObject>();
    }
    private void Update() 
    {
        PlayerVelocity();
        PlayerRotation();
        if (!GravityIsOn) 
        {
            CheckGravity();
        }        
    }
    private void PlayerVelocity() 
    {
        Vector3 _playerVelocity = new Vector3();

        if(_inputManager.MoveForwardButtonDown()) 
        {
            _playerVelocity += this.transform.forward;
        }
        if(_inputManager.MoveBackwardButtonDown()) 
        {
            _playerVelocity -= this.transform.forward;
        }
        if(_inputManager.MoveRightButtonDown()) 
        {
            _playerVelocity += this.transform.right;
        }
        if(_inputManager.MoveLeftButtonDown()) 
        {
            _playerVelocity -= this.transform.right;
        }
        if (_inputManager.FlyUpButtonDown())
        {
            _playerVelocity += this.transform.up;
            GravityIsOn = false;
        }
        if (_inputManager.FlyDownButtonDown())
        {
            _playerVelocity -= this.transform.up;
        }       

        _playerVelocity.Normalize();

        if (GravityIsOn)
        {
            _playerVelocity -= this.transform.up * GravityMultiplier;
        }

        _moveObject.GiveRigidbodyVelocity(_playerVelocity);
    }
    private void PlayerRotation() 
    {
        Vector3 _playerRotation = new Vector3();
        Vector3 _cameraRotation = new Vector3();
        
        if(_inputManager.CameraRotateUpButtonDown()) 
        {
            _cameraRotation.x -= 1;
        }
        if(_inputManager.CameraRotateDownButtonDown()) 
        {
            _cameraRotation.x += 1;
        }
        if(_inputManager.CameraRotateRightButtonDown()) 
        {
            _playerRotation.y += 1;
        }
        if(_inputManager.CameraRotateLeftButtonDown()) 
        {
            _playerRotation.y -= 1;
        }
        _rotateObjectPlayer.AddRotationToObject(_playerRotation);
        _rotateObjectcamera.AddRotationToObject(_cameraRotation);

    }
    private void CheckGravity()
    {
        if (OnGround())
        {
            GravityIsOn = true;
        }
    }
    private bool OnGround() 
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out _hit, _range, _layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * _range, Color.yellow);
            return true;
        }
        return false;
    }
}
