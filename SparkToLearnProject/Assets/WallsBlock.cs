using UnityEngine;

public class WallsBlock : MonoBehaviour
{
    [SerializeField]
    private bool _isLength;

    [SerializeField]
    public CodeblockReceiver[] receivers;

    private int[] walls;

    private void Start()
    {
        walls = new int[4];

        foreach(CodeblockReceiver receiver in receivers)
        {
            receiver.OnReceive.AddListener(UpdateWall);
        }

        foreach (CodeblockReceiver receiver in receivers)
        {
            receiver.OnDetach.AddListener(UpdateWall);
        }
    }

    private void UpdateWall()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (receivers[i].attacher != null)
            {
                walls[i] = receivers[i].attacher.transform.parent.GetComponent<WallBlock>()._wallIndex;
            } else
            {
                walls[i] = 0;
            }
        }

        transform.parent.GetComponent<HouseEditor>().Builder.SetWalls(walls, _isLength);
    }
}
