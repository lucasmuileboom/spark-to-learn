using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    [SerializeField] private Text[] _uiText;
    [SerializeField] private string _pressedText;

    public void Settext(int index)
    {    
        for(int i = 0; i < _uiText.Length; i++)
        {
            if (i == index)
            {
                _uiText[i].text = _pressedText;
            }
            else 
            {
                _uiText[i].text = " ";
            }
        }     
    }
}
