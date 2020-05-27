using UnityEngine;
using UnityEngine.UI;

public class WallList : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallButtonPrefab;
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private GameObject _wallBlockPrefab;

    [SerializeField]
    private ZoomUI _zoomUI;

    public void PopulateList(HouseBuilder.WallStruct[] walls)
    {
        for (int i = 0; i < walls.Length; i++)
        {
            GameObject wallButton = Instantiate(_wallButtonPrefab, _content);
            int index = i;
            string name = walls[i].Name;
            Sprite sprite = walls[i].Image;
            wallButton.GetComponentInChildren<Text>().text = name;
            wallButton.GetComponentsInChildren<Image>()[1].sprite = sprite;
            wallButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject wallBlock = Instantiate(_wallBlockPrefab, transform.parent);
                wallBlock.GetComponent<WallBlock>().SetupCodeblock(index, name, sprite, _zoomUI);
            });
        }
    }
}
