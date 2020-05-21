using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAddToSceneManager : MonoBehaviour
{
    private static int _priorityTurn = 0;

    [SerializeField] private SceneManager _sceneManager = null;
    [SerializeField] private ItemDetails _itemDetails = null;
    [SerializeField] [Range(0,10)] private int _priority = 0;

    private bool _triggered = false;

    void Update()
    {
        if (_sceneManager != null && _itemDetails != null && _priorityTurn >= _priority && !_triggered)
        {
            _sceneManager.AddObject(_itemDetails);
            _triggered = true;
            _priorityTurn++;
        }
    }
}
