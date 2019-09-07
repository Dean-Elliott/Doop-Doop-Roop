using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

//---------------------------------------------------------------------
// Name: AudioDriver
// Desc: This class times audio clips and invokes beat based movement
//---------------------------------------------------------------------

[RequireComponent (typeof(AudioSource))]
public class AudioDriver : MonoBehaviour
{  
    [Serializable]
    public class OnBeatWindowStarted : UnityEvent {}

    [Serializable]
    public class OnBeatWindowEnded : UnityEvent {}

    [Serializable]
    public class OnBeat : UnityEvent { }

    // Singleton access
    public static AudioDriver   instance { get; private set; }

    // Listen to these if you would like to know when you can move based on the beat
    public OnBeatWindowStarted  onBeatWindowStarted;
    public OnBeatWindowEnded    onBeatWindowEnded;

    public bool                 isInitialized { get; private set; }
    public bool                 isAudioClipPlaying { get { return _audioSource.isPlaying; } }

    [SerializeField]
    [Tooltip("This represents time in milliseconds after a beat in which a player can input a command")]
    // Average human reaction time is ~215 ms, so give it a bit of grace for the default value
    private int                 _beatWindowLength = 250;
    private int                 _clipBPM;
    private AudioSource         _audioSource;

    void Awake() {
        if (instance != null) {
            Debug.LogError("Multiple instances of the AudioDriver exits. Deleting the old one.");

            DestroyImmediate(instance);
        }

        instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        if (_audioSource == null) {
            Debug.LogError("Somehow the AudioDriver GameObject is missing an AudioSource component.");
        }

        isInitialized = false;
    }
    
    public void StartPlayback() {
        _audioSource.Play();

        StartCoroutine("BPMTicker");
    }

    public void StopPlayback() {
        _audioSource.Stop();

        StopCoroutine("BPMTicker");
    }

    public bool SetAudioClip(ref AudioClip newClip, int newClipBPM) {
        if (newClip == null || newClipBPM <= 0) {
            Debug.LogError("Attempting to set invalid AudioClip");
            return false;
        }

        if (_audioSource.isPlaying) {
            Debug.Log("Attempting to set new clip while current clip is playing. Stopping current clip");
            StopPlayback();
        }

        _audioSource.clip = newClip;
        _clipBPM = newClipBPM;

        isInitialized = true;
        return true;
    }

    public void ClearAudioClip() {
        _audioSource.clip = null;
        _clipBPM = 0;
        isInitialized = false;
    }

    private IEnumerator BPMTicker() {

        if (!isInitialized) {
            Debug.LogError("Attempting to start the BPM ticker without a valid audio clip. Bailing");
            yield break;
        }

        float secondsBetweenBeats = 1.0f / (_clipBPM / 60.0f);
        float inputWindowLengthInSeconds = _beatWindowLength / 1000.0f;

        while (_audioSource.isPlaying) {
            // This method assumes any valid audio clip begins with a beat.            
            onBeatWindowStarted.Invoke();

            // Note this is affected by time scale!
            yield return new WaitForSeconds(inputWindowLengthInSeconds);

            onBeatWindowEnded.Invoke();

            yield return new WaitForSeconds(secondsBetweenBeats - inputWindowLengthInSeconds);
        }
    }
}
