using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private static string _fileDirectory;

    private static AudioSource _audioSource;

    public static List<string> Files = new List<string>();
    private static List<AudioClip> _clips = new List<AudioClip>();

    private void Awake()
    {
        _fileDirectory = Path.Combine(Application.dataPath, @"..\", "Clips");

        // Create the custom audio directory if it does not exist
        if (!Directory.Exists(_fileDirectory))
        {
            Directory.CreateDirectory(_fileDirectory);
        }

        _audioSource = GetComponent<AudioSource>();

        UpdateFiles();
    }

    /// <summary>
    /// Clear the files and clips and fill them with audio files contained in the file directory
    /// </summary>
    public static void UpdateFiles()
    {
        Files.Clear();
        _clips.Clear();

        string[] files;
        files = Directory.GetFiles(_fileDirectory);

        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".wav"))
            {
                Files.Add(files[i]);
                _clips.Add(new WWW(files[i]).GetAudioClip(false, true, AudioType.WAV));
            }
        }
    }

    public static void PlayClip(int index)
    {
        _audioSource.PlayOneShot(_clips[index]);
    }
}
