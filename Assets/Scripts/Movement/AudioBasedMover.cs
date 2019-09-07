using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------
// Name: AudioBasedMover
// Desc: This abstract class moves GameObjects according to BPM
//---------------------------------------------------------------------

public abstract class AudioBasedMover : MonoBehaviour
{
    // For keeping movement atomic
    public enum MoveDirection {
        Up,
        Down,
        Left,
        Right
    }

    public bool canMove { get; private set; }

    protected virtual void Start() {
        if (AudioDriver.instance == null) {
            Debug.LogError("Could not find AudioDriver to synchronize. Destrying this mover.");
            DestroyImmediate(this);
        }
        else {
            AudioDriver.instance.onBeatWindowStarted.AddListener(OnBeatWindowStarted);
            AudioDriver.instance.onBeatWindowEnded.AddListener(OnBeatWindowEnded);
        }
    }

    // I would like to keep these private, but because this class is abstract I can't 
    // The best we can do is protected, so DON'T TOUCH THESE lest you break some movement logic
    protected virtual void OnBeatWindowStarted() {
        canMove = true;
    }

    protected virtual void OnBeatWindowEnded() {
        canMove = false;
    }

    // Handly little template method design pattern here :) 
    public bool Move(MoveDirection direction) {
        if (canMove) {

            transform.position += GetMoveDirectionVector(direction);

            // Ensures we only move once per valid beat window
            canMove = false;

            return true;
        }

        return false;
    }

    private Vector3 GetMoveDirectionVector(MoveDirection direction) {
        Vector3 moveVector = Vector3.zero;

        switch (direction) {
            case MoveDirection.Up: {
                    moveVector = Vector3.forward;
                    break;
                }
            case MoveDirection.Down: {
                    moveVector = -Vector3.forward;
                    break;
                }
            case MoveDirection.Left: {
                    moveVector = -Vector3.right;
                    break;
                }
            case MoveDirection.Right: {
                    moveVector = Vector3.right;
                    break;
                }
        }

        return moveVector;
    }
}
