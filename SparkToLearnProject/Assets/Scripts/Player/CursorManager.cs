using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private LockCursor _lockCursor;
    [SerializeField] private PlayerManager _PlayerManager;
    
    public void toggleCursor(bool newValue)
    {
        _lockCursor.toggleCursor(newValue);
        _PlayerManager.CanUseSkils = !newValue;
    }
}
