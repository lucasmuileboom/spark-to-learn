using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode _moveForwardButton;
    [SerializeField] KeyCode _moveBackwardButton;
    [SerializeField] KeyCode _moveLeftButton;
    [SerializeField] KeyCode _moveRightButton;

    [SerializeField] KeyCode _cameraRotateUpButton;
    [SerializeField] KeyCode _cameraRotateDownButton;
    [SerializeField] KeyCode _cameraRotateLeftButton;
    [SerializeField] KeyCode _cameraRotateRightButton;

    public bool _moveForwardButtonDown() 
    {
        return isButtonDown(_moveForwardButton);
    }
    public bool _moveBackwardButtonDown()
    {
        return isButtonDown(_moveBackwardButton);
    }
    public bool _moveLeftButtonDown() 
    {
        return isButtonDown(_moveLeftButton);
    }
    public bool _moveRightButtonDown() 
    {
        return isButtonDown(_moveRightButton);
    }
    public bool _cameraRotateUpButtonDown() 
    {
        return isButtonDown(_cameraRotateUpButton);
    }
    public bool _cameraRotateDownButtonDown() 
    {
        return isButtonDown(_cameraRotateDownButton);
    }
    public bool _cameraRotateLeftButtonDown() 
    {
        return isButtonDown(_cameraRotateLeftButton);
    }
    public bool _cameraRotateRightButtonDown() 
    {
        return isButtonDown(_cameraRotateRightButton);
    }
    private bool isButtonDown(KeyCode _keycode) 
    {
        return Input.GetKey(_keycode);
    }
}
