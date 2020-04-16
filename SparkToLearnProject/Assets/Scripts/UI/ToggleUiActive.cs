using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUiActive : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;

    public void ToggleUi()
    {
        _uiObject.SetActive(!_uiObject.activeSelf);
    }
    public void SetUi(bool newActive)
    {
        _uiObject.SetActive(newActive);
    }
}
