using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode _moveForwardButton;
    [SerializeField] private KeyCode _moveBackwardButton;
    [SerializeField] private KeyCode _moveLeftButton;
    [SerializeField] private KeyCode _moveRightButton;

    [SerializeField] private KeyCode _cameraRotateUpButton;
    [SerializeField] private KeyCode _cameraRotateDownButton;
    [SerializeField] private KeyCode _cameraRotateLeftButton;
    [SerializeField] private KeyCode _cameraRotateRightButton;

    [SerializeField] private KeyCode _FlyUpButton;
    [SerializeField] private KeyCode _FlyDownButton;

    public bool MoveForwardButtonDown() 
    {
        return IsButtonDown(_moveForwardButton);
    }
    public bool MoveBackwardButtonDown()
    {
        return IsButtonDown(_moveBackwardButton);
    }
    public bool MoveLeftButtonDown() 
    {
        return IsButtonDown(_moveLeftButton);
    }
    public bool MoveRightButtonDown() 
    {
        return IsButtonDown(_moveRightButton);
    }
    public bool CameraRotateUpButtonDown() 
    {
        return IsButtonDown(_cameraRotateUpButton);
    }
    public bool CameraRotateDownButtonDown() 
    {
        return IsButtonDown(_cameraRotateDownButton);
    }
    public bool CameraRotateLeftButtonDown() 
    {
        return IsButtonDown(_cameraRotateLeftButton);
    }
    public bool CameraRotateRightButtonDown() 
    {
        return IsButtonDown(_cameraRotateRightButton);
    }
    public bool FlyUpButtonDown()
    {
        return IsButtonDown(_FlyUpButton);
    }
    public bool FlyDownButtonDown()
    {
        return IsButtonDown(_FlyDownButton);
    }
    private bool IsButtonDown(KeyCode keycode) 
    {
        return Input.GetKey(keycode);
    }
}
