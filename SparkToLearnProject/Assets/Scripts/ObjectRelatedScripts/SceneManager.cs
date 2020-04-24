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

    public void AddObject(ItemDetails item)
    {
        _spawnedObjects.Add(item);

        GameObject uiItem = Instantiate<GameObject>(_itemUI, _container.transform);
        RectTransform uiTransform = uiItem.GetComponent<RectTransform>();

        uiItem.GetComponent<ItemDetailFiller>().SetItem(item);
        uiItem.GetComponent<ItemDetailFiller>().FillItemDetails();
        uiItem.GetComponent<ItemDetailFiller>().SetButtonActions(_onEdit, _onReplace, _onDelete);

        Invoke("ArrangeUI", 0.02f);
    }

    public void RemoveObject(ItemDetails item)
    {
        item = _spawnedObjects.Find((ItemDetails listItem)=> { return listItem.Instance.GetInstanceID() == item.Instance.GetInstanceID(); });
        int itemIndex = _spawnedObjects.FindIndex((ItemDetails listItem) => { return listItem.Instance.GetInstanceID() == item.Instance.GetInstanceID(); });
        _spawnedObjects.Remove(item);
        Destroy(_container.transform.GetChild(itemIndex).gameObject);
        Destroy(item.Instance);

        Invoke("ArrangeUI", 0.02f);
    }

    private void ArrangeUI()
    {
        int i = 0;
        foreach (RectTransform uiItem in _container.transform)
        {
            i++;
            uiItem.anchoredPosition = new Vector2(uiItem.anchoredPosition.x,
            -((i - 1) * uiItem.rect.height + _spacing * (i - 1)));
        }
    }
}
