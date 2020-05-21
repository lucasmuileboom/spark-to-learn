using System;
using UnityEngine;
using UnityEngine.UI;

public class HouseEditor : MonoBehaviour
{
    [HideInInspector]
    public HouseBuilder Builder;

    [Header("Size"), SerializeField]
    private Slider _lengthSlider;
    [SerializeField]
    private Slider _widthSlider;

    [SerializeField]
    private WallsBlock _lengthBlock;
    [SerializeField]
    private WallsBlock _widthBlock;

    [Header("Color"), SerializeField]
    private Slider[] _colorSliders;

    [SerializeField]
    private WallList _wallList;

    private void Start()
    {
        _wallList.PopulateList(Builder.WallPrefabs);

        AddSizeListeners();
        AddColorListeners();
    }

    private void AddSizeListeners()
    {
        _lengthSlider.onValueChanged.AddListener(delegate
        {
            Builder.Length = Convert.ToInt32(_lengthSlider.value);

            foreach(CodeblockReceiver receiver in _lengthBlock.receivers)
            {
                if (receiver.attacher != null)
                {
                    receiver.attacher.Detach();
                }
                receiver.gameObject.SetActive(false);
            }

            for (int i = 0; i < Builder.Length; i++)
            {
                if (i > 1 && i < Builder.Length)
                {
                    _lengthBlock.receivers[i-2].gameObject.SetActive(true);
                }
            }
        });

        _widthSlider.onValueChanged.AddListener(delegate
        {
            Builder.Width = Convert.ToInt32(_widthSlider.value);

            foreach (CodeblockReceiver receiver in _widthBlock.receivers)
            {
                if (receiver.attacher != null)
                {
                    receiver.attacher.Detach();
                }
                receiver.gameObject.SetActive(false);
            }

            for (int i = 0; i < Builder.Width; i++)
            {
                if (i > 1 && i < Builder.Width)
                {
                    _widthBlock.receivers[i - 2].gameObject.SetActive(true);
                }
            }
        });
    }

    private void AddColorListeners()
    {
        for (int i = 0; i < _colorSliders.Length; i++)
        {
            int index = i;
            _colorSliders[i].onValueChanged.AddListener(delegate
            {
                Builder.ChangeColor(index + 1, _colorSliders[index].value);
            });
        }
    }
}
