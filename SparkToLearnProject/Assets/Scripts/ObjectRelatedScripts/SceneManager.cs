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
    private Dictionary<string, List<ItemDetails>> _spawnedObjects;

    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _itemUI;
    [SerializeField] private GameObject _editOnlyItemUI;
    [SerializeField][Range(0,15)] private float _spacing = 7.5f;


    [SerializeField] private CursorManager _CursorManager;
    [SerializeField] private ItemEvent _onEdit;
    [SerializeField] private ItemEvent _onReplace;
    [SerializeField] private ItemEvent _onDelete;

    [SerializeField] private string _startCategory;
    [SerializeField] private List<string> _categories;
    [SerializeField] private List<bool> _editOnly;

    private string _currentCategory;

    void Start()
    {
        if(_categories.Count == 0)
        {
            _categories.Add("Default");
        }

        _currentCategory = (_startCategory == "") ? _categories[0] : _startCategory;

        _container = (!_container) ? gameObject : _container;
        _spawnedObjects = new Dictionary<string, List<ItemDetails>>(); 

        foreach(string category in _categories)
        {
            _spawnedObjects.Add(category, new List<ItemDetails>());
        }
    }

    public void AddObject(ItemDetails item)
    {
        string category = "Default";

        if (_spawnedObjects.ContainsKey(item.Category) || _spawnedObjects.Count == 0) category = item.Category;
        
        _spawnedObjects[category].Add(item);

        if (category != _currentCategory) return;

        GameObject uiItem = Instantiate<GameObject>(_itemUI, _container.transform);
        RectTransform uiTransform = uiItem.GetComponent<RectTransform>();

        uiItem.GetComponent<ItemDetailFiller>().SetItem(item);
        uiItem.GetComponent<ItemDetailFiller>().FillItemDetails();
        uiItem.GetComponent<ItemDetailFiller>().SetButtonActions(_onEdit, _onReplace, _onDelete);

        Invoke("ArrangeUI", 0.02f);
    }

    public void EditObject(ItemDetails item)
    {
        Debug.Log("Edit");
        if (item.gameObject.TryGetComponent<HouseBuilder>(out HouseBuilder blueprint))
        {
            blueprint.Editor.SetActive(true);
        }
        else
        {
            Blueprint _blueprint = item.gameObject.GetComponent<BlueprintObject>().Blueprint;

            _blueprint.Show();
        }
        
        _CursorManager.toggleCursor(true);
    }

    public void SwitchCategory(string category)
    {
        if (category == _currentCategory) return;

        GameObject _uiItem = (_editOnly[_categories.IndexOf(category)] == true) ? _editOnlyItemUI : _itemUI;

        foreach (RectTransform item in _container.transform)
        {
            Destroy(item.gameObject);
        }
        
        foreach (ItemDetails item in _spawnedObjects[category])
        {
            GameObject uiItem = Instantiate<GameObject>(_uiItem, _container.transform);
            RectTransform uiTransform = uiItem.GetComponent<RectTransform>();

            uiItem.GetComponent<ItemDetailFiller>().SetItem(item);
            uiItem.GetComponent<ItemDetailFiller>().FillItemDetails();
            uiItem.GetComponent<ItemDetailFiller>().SetButtonActions(_onEdit, _onReplace, _onDelete);
        }

        _currentCategory = category;

        Invoke("ArrangeUI", 0.02f);
    }

    public void RemoveObject(ItemDetails item)
    {

        string category = "Default";

        if (_spawnedObjects.ContainsKey(item.Category)) category = item.Category;

        item = _spawnedObjects[category].Find((ItemDetails listItem) => { return listItem.Instance.GetInstanceID() == item.Instance.GetInstanceID(); });
        int itemIndex = _spawnedObjects[category].FindIndex((ItemDetails listItem) => { return listItem.Instance.GetInstanceID() == item.Instance.GetInstanceID(); });

        _spawnedObjects[category].Remove(item);

        if(_currentCategory == item.Category)
        {
            Destroy(_container.transform.GetChild(itemIndex).gameObject);
        }

        Destroy(item.Instance);

        Invoke("ArrangeUI", 0.02f);
    }

    private void ArrangeUI()
    {
        int i = 0;
        foreach (RectTransform uiItem in _container.transform)
        {
            uiItem.anchoredPosition = new Vector2(uiItem.anchoredPosition.x,
            -(i * uiItem.rect.height + _spacing * i));
            i++;
        }
    }
}
