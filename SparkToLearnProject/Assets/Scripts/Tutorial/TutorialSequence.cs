using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class SegmentEvent : UnityEvent<int>{ }

public class TutorialSequence : MonoBehaviour
{
    [SerializeField]
    private CursorManager _cursorManager;

    [SerializeField]
    private List<TutorialSegment> _tutorialPages;

    [SerializeField]
    private bool _onlyOnce = true;
    private bool _triggered = false;
    [SerializeField]
    private bool _showOnStart = false;

    [SerializeField]
    private UnityEvent _onStart;

    [SerializeField]
    private SegmentEvent _onNext;

    [SerializeField]
    private SegmentEvent _onPrevious;

    [SerializeField]
    private UnityEvent _onEnd;


    void Awake()
    {
        for (int i = 0; i < _tutorialPages.Count; i++) {
            _tutorialPages[i].NextSegmentButton?.onClick.AddListener(() => { _onNext?.Invoke(i); });
            _tutorialPages[i].PreviousSegmentButton?.onClick.AddListener(() => { _onPrevious?.Invoke(i); });
            _tutorialPages[i].StopSequenceButton?.onClick.AddListener(() => { _onEnd?.Invoke(); });

            ToggleSegment(i, false);

        }
    }

    private void ToggleSegment(int segment, bool toggle)
    {
        if (_tutorialPages[segment].TryGetComponent<Image>(out Image img))
        {
            img.enabled = toggle;
        }
        if (_tutorialPages[segment].NextSegmentButton != null)
        {
            _tutorialPages[segment].NextSegmentButton.enabled = toggle;
            _tutorialPages[segment].NextSegmentButton.image.enabled = toggle;
        }
        if (_tutorialPages[segment].PreviousSegmentButton != null)
        {
            _tutorialPages[segment].PreviousSegmentButton.enabled = toggle;
            _tutorialPages[segment].PreviousSegmentButton.image.enabled = toggle;
        }
        if (_tutorialPages[segment].StopSequenceButton != null)
        {
            _tutorialPages[segment].StopSequenceButton.enabled = toggle;
            _tutorialPages[segment].StopSequenceButton.image.enabled = toggle;
        }
    }

    void Start()
    {
        if (_showOnStart)
        {
            Invoke("StartSequence",0.23f);
        }    
    }

    public void StartSequence()
    {
        if (_onlyOnce && !_triggered)
        {
            ToggleSegment(0, true);
            _cursorManager.toggleCursor(true);
            _onStart?.Invoke();
            _triggered = true;
        }
        if (!_onlyOnce)
        {
            ToggleSegment(0, true);
            _cursorManager.toggleCursor(true);
            _onStart?.Invoke();
        }
        
    }

    public void NextSegment(int segment)
    {
        ToggleSegment(Mathf.Min(segment + 1, _tutorialPages.Count - 1), true);
        ToggleSegment(segment, false);
    }

    public void PreviousSegment(int segment)
    {
        ToggleSegment(Mathf.Max(segment - 1, 0), true);
        if (segment != 0)
        {
            ToggleSegment(segment, false);
        }
    }

    public void StopSequence()
    {
        Debug.Log("Stop sequence");
        for(int i = 0; i < _tutorialPages.Count; i++)
        {
            ToggleSegment(i, false);
        }
        _cursorManager.toggleCursor(false);
    }
}
