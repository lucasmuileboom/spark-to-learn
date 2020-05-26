using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    [SerializeField]
    private ZoomUI _zoomUI;

    [SerializeField]
    private Image _image;
    [SerializeField]
    private Sprite[] _sprites;

    private void Start()
    {
        _zoomUI.OnScale.AddListener(UpdateImage);
    }

    private void UpdateImage()
    {
        _image.sprite = _sprites[_zoomUI.CurrentScaleFactor];
    }
}
