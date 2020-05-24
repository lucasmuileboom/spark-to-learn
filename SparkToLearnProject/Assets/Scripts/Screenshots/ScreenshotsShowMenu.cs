using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotsShowMenu : MonoBehaviour
{
    [SerializeField] private LockCursor _lockCursor;
    [SerializeField] private ScreenshotViewer _screenshotViewer;
    [SerializeField] private GameObject _screenshotViewerObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _screenshotViewerObject.SetActive(true);
            _lockCursor.toggleCursor(true);
            _screenshotViewer.PopulateGalleryWrapper();       
        }
    }
}
