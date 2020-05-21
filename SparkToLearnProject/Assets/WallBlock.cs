using UnityEngine;
using UnityEngine.UI;

public class WallBlock : MonoBehaviour
{
    public int _wallIndex;

    [SerializeField]
    private CodeblockAttacher _attacher;
    [SerializeField]
    private Text _text;

    public void SetupCodeblock(int wallIndex, string wallName)
    {
        _wallIndex = wallIndex;
        _text.text = wallName;
    }

    public void DestroyBlock()
    {
        _attacher.Detach();
        Destroy(gameObject);
    }
}
