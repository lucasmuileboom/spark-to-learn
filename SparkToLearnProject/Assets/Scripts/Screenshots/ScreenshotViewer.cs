using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotViewer : MonoBehaviour
{
    [SerializeField]
    private GameObject _screenshotThumbnailPrefab;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _screenshotView;

    public void PopulateGalleryWrapper()
    {
        ClearGallery();
        StartCoroutine(PopulateGallery());
    }

    private void ClearGallery()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            _content.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(_content.GetChild(i).gameObject);
        }
    }

    private IEnumerator PopulateGallery()
    {
        string[] files = Directory.GetFiles(Screenshot.screenshotPath, "*.png");

        for (int i = 0; i < files.Length; i++)
        {
            WWW www = new WWW(files[i]);
            while (!www.isDone)
            {
                yield return null;
            }

            GameObject screenshotThumbnail = Instantiate(_screenshotThumbnailPrefab, _content);

            screenshotThumbnail.GetComponent<RawImage>().texture = www.texture;
            screenshotThumbnail.GetComponent<Button>().onClick.AddListener(delegate () {
                _screenshotView.GetComponent<RawImage>().texture = screenshotThumbnail.GetComponent<RawImage>().texture;
                _screenshotView.SetActive(true);
            });
        }
    }
}
