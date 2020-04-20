using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private List<ItemDetails> _spawnedObjects;

    void Start()
    {
        
    }

    private void UpdateList()
    {
        _spawnedObjects.ForEach((ItemDetails listItem)=> {
            Debug.Log(listItem.name);
        });
    }

    public void AddObject(ItemDetails item)
    {
        if (!_spawnedObjects.Contains(item))
        {
            _spawnedObjects.Add(item);
            UpdateList();
        }
    }

    public void RemoveObject(ItemDetails item)
    {
        item = _spawnedObjects.Find((ItemDetails listItem)=> { return listItem == item; });
        _spawnedObjects.Remove(item);
        Destroy(item.Instance);
        UpdateList();
    }
}
