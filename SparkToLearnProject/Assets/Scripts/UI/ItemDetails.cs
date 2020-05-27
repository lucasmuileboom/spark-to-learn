using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    private bool _triggerTouch, _nonTriggerTouch = false;

    private void Start()
    {
        Rigidbody _rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }


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

    [SerializeField] private List<MeshRenderer> _mesh;
    public List<MeshRenderer> Mesh
    {
        get { return _mesh;  }
        private set { _mesh = Mesh; }
    }

    [SerializeField] private List<Collider> _colliders;
    public List<Collider> Colliders
    {
        get { return _colliders; }
        private set { _colliders = Colliders; }
    }

    private GameObject _instance;
    public GameObject Instance
    {
        get { return _instance; }
        private set { _instance = Instance; }
    }

    public void SetInstance(GameObject instance)
    {
        _instance = (_instance) ? _instance : instance;
    }

    public bool IsTriggerTouching()
    {
        return _triggerTouch;
    }
    public bool IsNonTriggerTouching()
    {
        return _nonTriggerTouch;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Prop")
        {
            _nonTriggerTouch = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Prop")
        {
            _nonTriggerTouch = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Prop")
        {
            _triggerTouch = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if(other.gameObject.tag == "Prop")
        {
            _triggerTouch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prop")
        {
            _triggerTouch = false;
        }
    }
}
