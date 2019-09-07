using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//---------------------------------------------------------------------
// Name: GameDriver
// Desc: This controls central game logic
//---------------------------------------------------------------------

public class GameDriver : MonoBehaviour
{
    [Serializable]
    public class GameTrack {
        public AudioClip audioClip;
        public int clipBPM;
    }

    public List<GameTrack> tracks;

    private void Start() {
        if (AudioDriver.instance == null) {
            Debug.LogError("GameDriver cannot find AudioDriver to start tracks.");
        }
        else {
            // Start off with the first track
            AdvanceTrack();
        }
    }

    private void AdvanceTrack() {
        // Yes, im aware this is basically just using a list as a queue, but the editor doesn't
        // serialize the System.Collections.Generic.Queue, and i'm too lazy to populate and maintain
        // an internal queue. Fight me.
        AudioDriver.instance.SetAudioClip(ref tracks[0].audioClip, tracks[0].clipBPM);

        tracks.RemoveAt(0);

        AudioDriver.instance.StartPlayback();
    }

    private void Update() {

        // As soon as the currently playing clip is done, play the next available clip.
        if (!AudioDriver.instance.isAudioClipPlaying && tracks.Count > 0) {
            AdvanceTrack();
        }
    }
}
