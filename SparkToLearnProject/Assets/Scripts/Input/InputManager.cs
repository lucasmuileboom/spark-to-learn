using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode _moveForwardButton;
    [SerializeField] KeyCode _moveBackwardButton;
    [SerializeField] KeyCode _moveLeftButton;
    [SerializeField] KeyCode _moveRightButton;

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
    private bool isButtonDown(KeyCode _keycode) 
    {
        return Input.GetKey(_keycode);
    }
}
