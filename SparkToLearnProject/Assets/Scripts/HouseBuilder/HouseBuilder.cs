using System;
using UnityEngine;

public class HouseBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _editorPrefab;
    private GameObject _editor;

    [Header("Size"), Range(2, 6)]
    public int Length = 2;
    private int _lastLength;
    [Range(2, 6)]
    public int Width = 2;
    private int _lastWidth;

    private Material _materialInstance;

    private Transform _foundationParent;
    [Header("Foundation"), SerializeField]
    private GameObject _foundationPrefab;
    private GameObject[,] _foundationArray;

    [Serializable]
    public struct WallStruct
    {
        public GameObject Prefab;
        public string Name;
        public Sprite Image;
    }

    private Transform _wallParent;
    [Header("Walls"), SerializeField]
    private GameObject _wallCorner;
    //public GameObject[] WallPrefabs;
    public WallStruct[] Walls;
    private int[] _lengthWalls;
    private int[] _widthWalls;

    private Transform _roofParent;
    [Header("Roof"), SerializeField]
    private GameObject _roofCornerPrefab;
    [SerializeField]
    private GameObject _roofEdgePrefab;
    [SerializeField]
    private GameObject _roofCenterPrefab;
    private GameObject[,] _roofArray;

    private BoxCollider _collider;

    private CursorManager _cursorManager;
    private PlayerManager _playerManager;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();

        _lengthWalls = new int[4];
        _widthWalls = new int[4];

        // Create material instance
        _materialInstance = Instantiate(_foundationPrefab.GetComponent<Renderer>().sharedMaterial);

        InstantiateEditor();
        InstantiateParents();
        BuildHouse();

        _lastLength = Length;
        _lastWidth = Width;

        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnMouseDown()
    {
        
        if (_playerManager.canEdit)
        {
            _editor.SetActive(true);

            _cursorManager.toggleCursor(true);
        }
    }

    private void Update()
    {
        if (_lastLength != Length || _lastWidth != Width)
        {
            // Center and scale the collider
            _collider.center = new Vector3(2.5f * (Length - 1), 5, 2.5f * (Width - 1));
            _collider.size = new Vector3(5 * Length, 10, 5 * Width);

            BuildHouse();

            _lastLength = Length;
            _lastWidth = Width;
        }
    }

    private void InstantiateEditor()
    {
        _editor = Instantiate(_editorPrefab, FindObjectOfType<Canvas>().transform);
        _editor.GetComponent<HouseEditor>().Builder = this;
        _editor.SetActive(false);
    }

    private void InstantiateParents()
    {
        _foundationParent = new GameObject("FoundationParent").transform;
        _foundationParent.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _foundationParent.parent = transform;

        _wallParent = new GameObject("WallParent").transform;
        _wallParent.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _wallParent.parent = transform;

        _roofParent = new GameObject("RoofParent").transform;
        _roofParent.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _roofParent.parent = transform;
    }

    public void SetWalls(int[] walls, bool isLength)
    {
        if (isLength)
        {
            _lengthWalls = walls;
        }
        else
        {
            _widthWalls = walls;
        }

        BuildHouse();
    }

    private void BuildHouse()
    {
        BuildFoundation();
        BuildWalls();
        BuildRoof();
    }

    private void BuildFoundation()
    {
        // Empty the array
        _foundationArray = new GameObject[Length, Width];

        // Destroy all previous foundation
        DestroyAllChildren(_foundationParent);

        // Generate foundation
        for (int l = 0; l < Length; l++)
        {
            for (int w = 0; w < Width; w++)
            {
                _foundationArray[l, w] = Instantiate(_foundationPrefab, new Vector3(transform.position.x + 5 * l, transform.position.y, transform.position.z + 5 * w), Quaternion.identity, _foundationParent);
                _foundationArray[l, w].name = "Foundation (" + l + " - " + w + ")";
                // Assign material
                _foundationArray[l, w].GetComponent<Renderer>().material = _materialInstance;
            }
        }
    }

    private void BuildWalls()
    {
        // Destroy all previous walls
        DestroyAllChildren(_wallParent);

        // Generate the roof
        for (int l = 0; l < Length; l++)
        {
            for (int w = 0; w < Width; w++)
            {
                if (l == 0 || w == 0 || l == Length - 1 || w == Width - 1)
                {
                    // Determine the right roof piece
                    GameObject wallPiece = Walls[0].Prefab;
                    if ((l == 0 && w == 0) || (l == 0 && w == Width - 1) || (l == Length - 1 && w == 0) || (l == Length - 1 && w == Width - 1))
                    {
                        wallPiece = _wallCorner;
                    }
                    else if (l == 0 || w == 0 || l == Length - 1 || w == Width - 1)
                    {
                        if (w == 0 || w == Width - 1)
                        {
                            wallPiece = Walls[_lengthWalls[l - 1]].Prefab;
                        }
                        else if (l == 0 || l == Length - 1)
                        {
                            wallPiece = Walls[_widthWalls[w - 1]].Prefab;
                        }
                    }

                    // Determine the right rotation
                    Quaternion rotation = Quaternion.identity;
                    if (l == Length - 1 && w < Width - 1)
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (w == Width - 1 && l > 0)
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (w == 0)
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }

                    // Instantiate the roof
                    GameObject wall = Instantiate(wallPiece, new Vector3(transform.position.x + 5 * l, transform.position.y, transform.position.z + 5 * w), rotation, _wallParent);

                    // Assign material
                    if (wall.GetComponent<Renderer>())
                    {
                        wall.GetComponent<Renderer>().material = _materialInstance;
                    }
                    else
                    {
                        wall.GetComponentInChildren<Renderer>().material = _materialInstance;
                    }
                }
            }
        }
    }

    private void BuildRoof()
    {
        // Empty the array
        _roofArray = new GameObject[Length, Width];

        // Destroy all previous walls
        DestroyAllChildren(_roofParent);

        // Generate the roof
        for (int l = 0; l < Length; l++)
        {
            for (int w = 0; w < Width; w++)
            {
                // Determine the right roof piece
                GameObject roofPiece;
                if ((l == 0 && w == 0) || (l == 0 && w == Width - 1) || (l == Length - 1 && w == 0) || (l == Length - 1 && w == Width - 1))
                {
                    roofPiece = _roofCornerPrefab;
                }
                else if (l == 0 || w == 0 || l == Length - 1 || w == Width - 1)
                {
                    roofPiece = _roofEdgePrefab;
                }
                else
                {
                    roofPiece = _roofCenterPrefab;
                }

                // Determine the right rotation
                Quaternion rotation = Quaternion.identity;
                if (l == Length - 1 && w < Width - 1)
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (w == Width - 1 && l > 0)
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (w == 0)
                {
                    rotation = Quaternion.Euler(0, -90, 0);
                }

                // Instantiate the roof
                _roofArray[l, w] = Instantiate(roofPiece, new Vector3(transform.position.x + 5 * l, transform.position.y, transform.position.z + 5 * w), rotation, _roofParent);
                _roofArray[l, w].name = "Roof (" + l + " - " + w + ")";

                // Assign material
                if (_roofArray[l, w].GetComponent<Renderer>())
                {
                    _roofArray[l, w].GetComponent<Renderer>().material = _materialInstance;
                }
                else
                {
                    _roofArray[l, w].GetComponentInChildren<Renderer>().material = _materialInstance;
                }
            }
        }
    }

    /// <summary>
    /// WHAT DO I DO LORD? 
    /// DESTROY THE CHILD. CORRUPT THEM ALL.
    /// THIS IS THEIR PLAN, PEOPLE. THESE PEOPLE ARE DEMONS!!!
    /// </summary>
    private void DestroyAllChildren(Transform trans)
    {
        foreach (Transform child in trans)
        {
            Destroy(child.gameObject);
        }

        // TODO: corrupt them all
    }

    public void ChangeColor(int colorIndex, float newH)
    {
        float h;
        float s;
        float v;
        Color.RGBToHSV(_materialInstance.GetColor("_Mask" + colorIndex + "_color"), out h, out s, out v);

        if (s == 0)
        {
            s = 1;
        }

        h = newH;

        _materialInstance.SetColor("_Mask" + colorIndex + "_color", Color.HSVToRGB(h, s, v));
    }
}
