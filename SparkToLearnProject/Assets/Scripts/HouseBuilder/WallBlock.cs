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

    public void SetupCodeblock(int wallIndex, string wallName, Sprite wallSprite)
    {
        _wallIndex = wallIndex;
        _text.text = wallName;
        _image.sprite = wallSprite;
    }

    public void DestroyBlock()
    {
        _attacher.Detach();
        Destroy(gameObject);
    }
}
