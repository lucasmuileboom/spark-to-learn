using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetButtonPressedUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonPressedGameObject;
    public void Settext(int index)
    {
        for (int i = 0; i < _buttonPressedGameObject.Length; i++)
        {
            if (i == index)
            {
                _buttonPressedGameObject[i].SetActive(true);
            }
            else 
            {
                _buttonPressedGameObject[i].SetActive(false);
            }
        }     
    }
}
