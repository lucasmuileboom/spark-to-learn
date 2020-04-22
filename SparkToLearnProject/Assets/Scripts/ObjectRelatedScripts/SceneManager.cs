using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class ItemEvent : UnityEvent<ItemDetails> { }

public class SceneManager : MonoBehaviour
{
    private List<ItemDetails> _spawnedObjects;

    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _itemUI;
    [SerializeField][Range(0,15)] private float _spacing = 7.5f;

    [SerializeField] private ItemEvent _onEdit;
    [SerializeField] private ItemEvent _onReplace;
    [SerializeField] private ItemEvent _onDelete;


    void Start()
    {
        _container = (!_container) ? gameObject : _container;
        _spawnedObjects = new List<ItemDetails>();
    }

    private void UpdateList()
    {
        foreach (Transform item in _container.transform)
        {
            Destroy(item.gameObject);
        }

        int i = 0;
        _spawnedObjects.ForEach((ItemDetails listItem) => {
            i++;
            GameObject uiItem = Instantiate<GameObject>(_itemUI,_container.transform);
            RectTransform uiTransform = uiItem.GetComponent<RectTransform>();

            uiItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiTransform.anchoredPosition.x,
                -((i-1)*uiTransform.rect.height+_spacing*(i-1)));
            uiItem.GetComponent<ItemDetailFiller>().FillItemDetails(listItem);
            uiItem.GetComponent<ItemDetailFiller>().SetButtonActions(_onEdit,_onReplace,_onDelete);
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
        Debug.Log("Remove");
        item = _spawnedObjects.Find((ItemDetails listItem)=> { return listItem.Instance.GetInstanceID() == item.Instance.GetInstanceID(); });
        _spawnedObjects.Remove(item);
        Destroy(item.Instance);
        UpdateList();
    }
}
