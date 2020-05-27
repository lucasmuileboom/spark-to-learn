using UnityEngine;
using UnityEngine.UI;

public class WallBlock : MonoBehaviour
{
    public int _wallIndex;

    [SerializeField]
    private CodeblockAttacher _attacher;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Image _image;

    private ZoomUI _zoomUI;

    public void SetupCodeblock(int wallIndex, string wallName, Sprite wallSprite, ZoomUI zoomUI)
    {
        _wallIndex = wallIndex;
        _text.text = wallName;
        _image.sprite = wallSprite;
        _zoomUI = zoomUI;

        _zoomUI.AddZoomable(transform);
    }

    public void DestroyBlock()
    {
        _attacher.Detach();

        _zoomUI.RemoveZoomable(transform);

        Destroy(gameObject);
    }
}
