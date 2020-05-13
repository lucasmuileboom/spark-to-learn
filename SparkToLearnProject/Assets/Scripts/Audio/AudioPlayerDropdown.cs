using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fills a dropdown with files from AudioPlayer.cs
/// </summary>
public class AudioPlayerDropdown : MonoBehaviour
{
    private Dropdown _dropdown;

    private void Start()
    {
        _dropdown = GetComponent<Dropdown>();

        for (int i = 0; i < AudioPlayer.Files.Count; i++)
        {
            string text = Path.GetFileName(AudioPlayer.Files[i]);
            _dropdown.options.Add(new Dropdown.OptionData() { text = text });
        }
    }
}
