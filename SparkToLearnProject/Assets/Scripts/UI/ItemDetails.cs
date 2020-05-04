using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] private string _name;
    public string Name
    {
        get { return _name; }
        private set { _name = Name; }
    }

    [SerializeField] private GameObject _objectReference;
    public GameObject ObjectReference
    {
        get { return _objectReference; }
        private set { _objectReference = ObjectReference; }
    }

    [SerializeField] private string _category;
    public string Category
    {
        get { return _category; }
        private set { _category = Category; }
    }

    [SerializeField] private string _description;
    public string Description
    {
        get { return _description; }
        private set { _description = Description; }
    }

    [SerializeField] private Sprite _thumbnail;
    public Sprite Thumbnail
    {
        get { return _thumbnail; }
        private set { _thumbnail = Thumbnail; }
    }

    private GameObject _instance;
    public GameObject Instance
    {
        get { return _instance; }
        private set { _instance = Instance; }
    }

    public GameObject InstantiateSelf(Transform spawnPoint)
    {
        _instance = (_instance) ? _instance : Instantiate<GameObject>(_objectReference,spawnPoint.position, spawnPoint.rotation);
        return _instance;
    }
}
