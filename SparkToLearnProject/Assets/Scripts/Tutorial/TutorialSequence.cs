using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialSequence : MonoBehaviour
{
    [SerializeField]
    private List<TutorialSegment> _tutorialPages;

    [SerializeField]
    private bool _onlyOnce = true;

    [SerializeField]
    private UnityEvent _onStart;

    [SerializeField]
    private UnityEvent<int> _onNext;

    [SerializeField]
    private UnityEvent<int> _onPrevious;

    [SerializeField]
    private UnityEvent _onEnd;


    private void Awake()
    {
        for (int i = 0; i < _tutorialPages.Count; i++) {
            _tutorialPages[i].NextSegmentButton?.onClick.AddListener(() => { _onNext?.Invoke(i); });
            _tutorialPages[i].PreviousSegmentButton?.onClick.AddListener(() => { _onPrevious?.Invoke(i); });
            _tutorialPages[i].StopSequenceButton?.onClick.AddListener(() => { _onEnd?.Invoke(); });
            _tutorialPages[i].enabled = false;
        }
    }

    public void StartSequence()
    {
        _tutorialPages[0].enabled = true;
        _onStart?.Invoke();
    }

    public void NextSegment(int segment)
    {
        _tutorialPages[Mathf.Min(segment + 1,_tutorialPages.Count-1)].enabled = true;
        _tutorialPages[segment].enabled = false;
    }
    public void PreviousSegment(int segment)
    {
        _tutorialPages[Mathf.Max(segment - 1,0)].enabled = true;
        _tutorialPages[segment].enabled = false;
    }
    public void StopSequence()
    {
        foreach(TutorialSegment segment in _tutorialPages)
        {
            segment.enabled = false;
        }
    }
}
