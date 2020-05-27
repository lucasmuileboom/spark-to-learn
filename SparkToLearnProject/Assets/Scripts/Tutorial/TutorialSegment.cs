using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSegment : MonoBehaviour
{
    [SerializeField] private Button _nextSegmentButton;
    public Button NextSegmentButton
    {
        get { return _nextSegmentButton; }
        private set { _nextSegmentButton = NextSegmentButton; } 
    }

    [SerializeField] private Button _previousSegmentButton;
    public Button PreviousSegmentButton
    {
        get { return _previousSegmentButton; }
        private set { _previousSegmentButton = PreviousSegmentButton; }
    }

    [SerializeField] private Button _stopSequenceButton;
    public Button StopSequenceButton
    {
        get { return _stopSequenceButton; }
        private set { _stopSequenceButton = StopSequenceButton; }
    }
}
