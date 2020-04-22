using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemDetailFiller : MonoBehaviour
{
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _replaceButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Image _image;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;

    private ItemDetails _item;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillItemDetails(ItemDetails item)
    {
        _item = item;
        _image.sprite = item.Thumbnail;
        _name.text = item.Name;
        _description.text = item.Description;
    }

    public void SetButtonActions(ItemEvent editEvent, ItemEvent replaceEvent, ItemEvent deleteEvent)
    {
        Debug.Log(_item);
        if (_item != null)
        {
            _editButton.onClick.AddListener(() => editEvent?.Invoke(_item));
            _replaceButton.onClick.AddListener(() => replaceEvent?.Invoke(_item));
            _deleteButton.onClick.AddListener(() => deleteEvent?.Invoke(_item));
        }
    }
}
