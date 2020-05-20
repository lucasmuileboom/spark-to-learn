using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAddToSceneManager : MonoBehaviour
{
    private static int priorityTurn = 0;
    [SerializeField] private SceneManager _sceneManager = null;
    [SerializeField] private ItemDetails _itemDetails = null;
    [SerializeField] [Range(0,10)] private int priority = 0;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (_sceneManager != null && _itemDetails != null && priorityTurn == priority)
        {
            Debug.Log(_itemDetails.Category);
            _sceneManager.AddObject(_itemDetails);
            priorityTurn++;
        }
    }
}
