using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------
// Name: AudioBasedMover
// Desc: This abstract class moves GameObjects according to BPM
//---------------------------------------------------------------------

public abstract class AudioBasedMover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The distance we cast a ray to check for collisions before moving")]
    public static float movementRaycastDistance = 1.5f;

    // For keeping movement atomic
    public enum MoveDirection {
        Invalid,
        Up,
        Down,
        Left,
        Right
    }

    public bool isOnBeat { get; private set; }

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
        isOnBeat = true;
    }

    protected virtual void OnBeatWindowEnded() {
        isOnBeat = false;
    }

    public bool Move(MoveDirection direction) {
        if (isOnBeat && CanMoveinDirection(direction)) {

            Vector3 moveDirection = GetMoveDirectionVector(direction);

            if (moveDirection != Vector3.zero) {
                transform.position += moveDirection;

                // Ensures we only move once per valid beat window
                isOnBeat = false;

                return true;
            }
        }

        if (!isOnBeat) {
            Debug.Log("Could not move because off beat.");
        }

        if (!CanMoveinDirection(direction)) {
            Debug.Log("Could not move because blocked");
        }
        return false;
    }

    private bool CanMoveinDirection(MoveDirection desiredDirection) {
        // TODO: Set up the layer mask for this raycast to save on physics calculations
        return !Physics.Raycast(transform.position, GetMoveDirectionVector(desiredDirection), movementRaycastDistance);
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
            default: {
                break;
            }
        }

        return moveVector;
    }
}
