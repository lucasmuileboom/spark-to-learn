using UnityEngine;
using UnityEngine.UI;

public class Codeblock : MonoBehaviour
{
    [SerializeField]
    private Dropdown _functionDropdown;

    [SerializeField]
    private GameObject _colorInput;
    [SerializeField]
    private Dropdown _colorDropdown;

    [SerializeField]
    private GameObject _vectorInput;
    [SerializeField]
    private InputField xInput;
    [SerializeField]
    private InputField yInput;
    [SerializeField]
    private InputField zInput;

    private enum BlockFunction { Start, ChangeColor, Move, Scale, Rotate }

    [SerializeField]
    private BlockFunction _blockFunction;

    [SerializeField]
    private CodeblockAttacher attacher;
    [SerializeField]
    private CodeblockReceiver receiver;

    public void ExecuteCodeblock(GameObject obj)
    {
        switch(_blockFunction)
        {
            case BlockFunction.Start:
                Debug.Log("Starting blueprint");
                break;
            case BlockFunction.ChangeColor:
                Color c = Color.white;

                switch (_colorDropdown.value)
                {
                    case 0:
                        c = Color.red;
                        break;
                    case 1:
                        c = Color.green;
                        break;
                    case 2:
                        c = Color.blue;
                        break;
                }

                obj.GetComponent<Renderer>().material.color = c;
                break;
            case BlockFunction.Move:
                obj.transform.position = new Vector3(int.Parse(xInput.text), int.Parse(yInput.text), int.Parse(zInput.text));
                break;
            case BlockFunction.Scale:
                obj.transform.localScale = new Vector3(int.Parse(xInput.text), int.Parse(yInput.text), int.Parse(zInput.text));
                break;
            case BlockFunction.Rotate:
                obj.transform.rotation = Quaternion.Euler(new Vector3(int.Parse(xInput.text), int.Parse(yInput.text), int.Parse(zInput.text)));
                break;
        }

        if (attacher.attachedReceiver)
        {
            attacher.attachedReceiver.codeblock.ExecuteCodeblock(obj);
        }
    }

    public void SetCodeblockFunction(int i)
    {
        _blockFunction = (BlockFunction)_functionDropdown.value+1;

        if(_blockFunction == BlockFunction.ChangeColor)
        {
            _colorInput.SetActive(true);
            _vectorInput.SetActive(false);
        } else
        {
            _colorInput.SetActive(false);
            _vectorInput.SetActive(true);
        }
    }

    public void RemoveCodeBlock()
    {
        attacher.Detach();
        if (receiver.attacher)
        {
            receiver.attacher.Detach();
        }

        Destroy(gameObject);
    }
}
