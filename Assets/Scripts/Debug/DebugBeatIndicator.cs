using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBeatIndicator : MonoBehaviour
{
    private Image _debugImage;

    private void Start() {
        _debugImage = GetComponent<Image>();

        if (AudioDriver.instance != null) {
            AudioDriver.instance.onBeatWindowStarted.AddListener(OnBeatWindowStarted);
            AudioDriver.instance.onBeatWindowEnded.AddListener(OnBeatWindowEnded);
        }
    }

    private void OnBeatWindowStarted() {
        _debugImage.color = Color.green;
    }

    private void OnBeatWindowEnded() {
        _debugImage.color = Color.white;
    }
}
