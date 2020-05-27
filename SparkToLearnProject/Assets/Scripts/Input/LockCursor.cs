using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursor : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _crossHair;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void toggleCursor(bool Cursoron)
    {
        Cursor.visible = Cursoron;
        _playerManager.canRotate = !Cursoron;
        _crossHair.SetActive(!Cursoron);
        if (!Cursoron)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }        
    }
}
