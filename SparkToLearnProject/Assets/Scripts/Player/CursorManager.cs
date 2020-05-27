using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private LockCursor _lockCursor;
    [SerializeField] private PlayerManager _playerManager;
    
    public void toggleCursor(bool newValue)
    {
        _lockCursor.toggleCursor(newValue);
        _playerManager.canUseSkils = !newValue;
    }
}
