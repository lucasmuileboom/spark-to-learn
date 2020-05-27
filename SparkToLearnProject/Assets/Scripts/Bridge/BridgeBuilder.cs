using UnityEngine;
using UnityEngine.EventSystems;

public class BridgeBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _editorPrefab;

    private GameObject _editor;

    [SerializeField]
    private GameObject[] _bridges;

    public int StartType;

    private CursorManager _cursorManager;
    private PlayerManager _playerManager;

    private void Awake()
    {
        _cursorManager = GameObject.Find("Canvas").GetComponent<CursorManager>();
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void Start()
    {
        _editor = Instantiate(_editorPrefab, FindObjectOfType<Canvas>().transform);
        _editor.GetComponent<BridgeEditor>().BridgeBuilder = this;
        _editor.SetActive(false);

        SetType(StartType);
    }

    private void OnMouseDown()
    {
        if (_playerManager.canEdit && !EventSystem.current.IsPointerOverGameObject())
        {
            _editor.SetActive(true);

            _cursorManager.toggleCursor(true);
        }
    }

    public void SetColor(float[] hVals)
    {
        Shader shader = Shader.Find("Custom/UltraShader");

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.material.shader == shader)
            {
                for (int i = 0; i < hVals.Length; i++)
                {
                    float h;
                    float s;
                    float v;
                    Color.RGBToHSV(r.material.GetColor("_Mask" + (i + 1) + "_color"), out h, out s, out v);

                    if (s == 0)
                    {
                        s = 1;
                    }

                    h = hVals[i];

                    r.material.SetColor("_Mask" + (i + 1) + "_color", Color.HSVToRGB(h, s, v));
                }
            }
        }
    }

    public void SetType(int i)
    {
        foreach (GameObject go in _bridges)
        {
            go.SetActive(false);
        }

        _bridges[i].SetActive(true);
    }
}

