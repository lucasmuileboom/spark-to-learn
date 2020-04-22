using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemListCycle : MonoBehaviour
{
    [SerializeField] private GameObject _previousItemThumbnailHolder;
    [SerializeField] private GameObject _currentItemThumbnailHolder;
    [SerializeField] private GameObject _nextItemThumbnailHolder;

    [SerializeField] private bool _startIndexIsHalfLength;
    [SerializeField] private List<ItemDetails> _items;

    [SerializeField] private UnityEvent _onItemSelectChange;

    private int _index;

    void Start()
    {
        _index = (_startIndexIsHalfLength) ? _items.Count/2 : 1;

        _previousItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index - 1 + _items.Count)% _items.Count].Thumbnail;
        _currentItemThumbnailHolder.GetComponent<Image>().sprite = _items[_index].Thumbnail;
        _nextItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index + 1 + _items.Count)% _items.Count].Thumbnail;
    }

    public void NextItem()
    {
        _index = (_index + 1 + _items.Count) % _items.Count;

        _previousItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index - 1 + _items.Count)% _items.Count].Thumbnail;
        _currentItemThumbnailHolder.GetComponent<Image>().sprite = _items[_index].Thumbnail;
        _nextItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index + 1 + _items.Count)% _items.Count].Thumbnail;

        _onItemSelectChange?.Invoke();
    }

    public void PreviousItem()
    {
        _index = (_index - 1 + _items.Count) % _items.Count;

        _previousItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index - 1 + _items.Count)% _items.Count].Thumbnail;
        _currentItemThumbnailHolder.GetComponent<Image>().sprite = _items[_index].Thumbnail;
        _nextItemThumbnailHolder.GetComponent<Image>().sprite = _items[(_index + 1 + _items.Count)%_items.Count].Thumbnail;

        _onItemSelectChange?.Invoke();
    }

    public ItemDetails GetItem()
    {
        return _items[_index];
    }
}
