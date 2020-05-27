using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Fills a dropdown with files from AudioPlayer.cs
/// </summary>
public class AudioPlayerDropdown : MonoBehaviour, IPointerEnterHandler
{
    private Dropdown _dropdown;

    private void Start()
    {
        _dropdown = GetComponent<Dropdown>();

        UpdateDropDown();
    }

    private void UpdateDropDown()
    {
        _dropdown.options.Clear();

        for (int i = 0; i < AudioPlayer.Files.Count; i++)
        {
            string text = Path.GetFileName(AudioPlayer.Files[i]);
            _dropdown.options.Add(new Dropdown.OptionData() { text = text });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDropDown();
    }
}
