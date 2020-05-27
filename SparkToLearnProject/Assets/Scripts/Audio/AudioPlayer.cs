using NAudio.Wave;
using SFB;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public static string FileDirectory;

    private static AudioSource _audioSource;

    public static List<string> Files = new List<string>();
    private static List<AudioClip> _clips = new List<AudioClip>();

    private void Awake()
    {
        FileDirectory = Path.Combine(Application.dataPath, @"..\", "Clips");

        // Create the custom audio directory if it does not exist
        if (!Directory.Exists(FileDirectory))
        {
            Directory.CreateDirectory(FileDirectory);
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
        files = Directory.GetFiles(FileDirectory);

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

    public static void OpenAudioFile()
    {
        var extensions = new[] {
            new ExtensionFilter("Sound Files", "mp3", "wav" ),
        };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        if (path.Length == 0)
        {
            return;
        }

        if (path[0].EndsWith(".mp3"))
        {
            Mp3ToWav(path[0], FileDirectory + @"\" + Path.GetFileNameWithoutExtension(path[0]) + ".wav");
        }
        else
        {
            if (!File.Exists(FileDirectory + @"\" + Path.GetFileName(path[0])))
            {
                File.Copy(path[0], FileDirectory + @"\" + Path.GetFileName(path[0]));
            }
        }

        UpdateFiles();
    }

    public static void Mp3ToWav(string mp3File, string outputFile)
    {
        using (Mp3FileReader reader = new Mp3FileReader(mp3File))
        {
            using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
            {
                WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
            }
        }
    }
}
