using System;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public static string screenshotPath;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        screenshotPath = Path.Combine(Application.dataPath, @"..\", "Screenshots");

        // Create screenshot path if it doesn't exist
        if (!Directory.Exists(screenshotPath))
        {
            Directory.CreateDirectory(screenshotPath);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        // Get date and format to be compatible with file naming
        string screenshotDate = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
        screenshotDate = screenshotDate.Replace("/", "-");
        screenshotDate = screenshotDate.Replace(" ", "_");
        screenshotDate = screenshotDate.Replace(":", "-");

        string screenshotName = "SparkToLearn - " + screenshotDate;

        string screenshotExtension = ".png";

        try
        {
            ScreenCapture.CaptureScreenshot(Path.Combine(screenshotPath, screenshotName) + screenshotExtension);

            _audioSource.Play();

            Debug.Log("Screenshot captured at \"" + Path.Combine(screenshotPath, screenshotName) + screenshotExtension + "\"");
        }
        catch (Exception e)
        {
            Debug.Log("Screenshot failed with exception: " + e);
        }
    }
}