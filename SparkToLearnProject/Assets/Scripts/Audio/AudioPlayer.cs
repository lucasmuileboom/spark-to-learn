using System;
using System.Collections.Generic;
using System.IO;
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
        if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "SparkToLearn")))
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "SparkToLearn"));
        }

        _fileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "SparkToLearn");
        _audioSource = GetComponent<AudioSource>();

        UpdateFiles();
    }

    public static void UpdateFiles()
    {
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
