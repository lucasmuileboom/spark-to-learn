using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoffCursor : MonoBehaviour
{
    private CursorManager _cursorManager;
    private void Start()
    {
        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
    }
    public void toggleCursor()
    {
        _cursorManager.toggleCursor(false);
    }
}
