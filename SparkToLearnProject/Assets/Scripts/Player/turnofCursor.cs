using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnofCursor : MonoBehaviour
{
    [SerializeField] private CursorManager _CursorManager;
    private void Start()
    {
        _CursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
    }
        public void toggleCursor()
    {
        _CursorManager.toggleCursor(false);
    }
}
