using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemDetailFiller : MonoBehaviour
{
    [SerializeField] private Button _editButton = null;
    [SerializeField] private Button _replaceButton = null;
    [SerializeField] private Button _deleteButton = null;
    [SerializeField] private Image _image;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;

    private ItemDetails _item = null;

    public void SetItem(ItemDetails item)
    {
        _item = item;
    }

    public void FillItemDetails()
    {
        _image.sprite = _item.Thumbnail;
        _name.text = _item.Name;
        _description.text = _item.Description;
    }

    public void SetButtonActions(ItemEvent editEvent, ItemEvent replaceEvent, ItemEvent deleteEvent)
    {
        if (_item != null)
        {
            if (_editButton != null)
            {
                _editButton.onClick.AddListener(() => editEvent?.Invoke(_item));
            }
            if (_replaceButton != null)
            {
                _replaceButton.onClick.AddListener(() => replaceEvent?.Invoke(_item));
            }
            if (_deleteButton != null)
            {
                _deleteButton.onClick.AddListener(() => deleteEvent?.Invoke(_item));
            }
        }
        
        
    }
}
