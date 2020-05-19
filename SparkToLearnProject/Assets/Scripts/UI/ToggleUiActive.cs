using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUiActive : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private Image[] _uiObjects;
    [SerializeField] private bool SceneManger;
    [SerializeField] private bool Uupdate;

    public void ToggleUi()
    {
        _uiObject.SetActive(!_uiObject.activeSelf);
    }
    public void SetUi(bool newActive)
    {
       
        if (!SceneManger)
        {
            _uiObject.SetActive(newActive);
        }
        else 
        {
            Uupdate = newActive;
            foreach (Transform child in _uiObject.transform)
            {
                child.gameObject.SetActive(newActive);
            }
            foreach (Image ui in _uiObjects) 
            {
                ui.enabled = newActive;
            }
           
        }        
    }
    private void Update()
    {
        if (SceneManger)
        {
            foreach (Transform child in _uiObject.transform)
            {
                child.gameObject.SetActive(Uupdate);
            }
        }
       
    }
}
