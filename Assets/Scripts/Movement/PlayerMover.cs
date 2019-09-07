using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------
// Name: PlayerMover
// Desc: This controls player movement according to BPM
//---------------------------------------------------------------------

public class PlayerMover : AudioBasedMover
{
    private KeyCode _upKeyCode       = KeyCode.W;
    private KeyCode _downKeyCode     = KeyCode.S;
    private KeyCode _leftKeyCode     = KeyCode.A;
    private KeyCode _rightKeyCode    = KeyCode.D;

    // Update is called once per frame
    void Update()
    {
        // TODO: Maybe add an "Invalid" to MoveDirection so that I don't have to do this?
        MoveDirection newDirection = MoveDirection.Invalid;

        if (Input.GetKeyDown(_upKeyCode)) {
            newDirection = MoveDirection.Up;

        } else if (Input.GetKeyDown(_downKeyCode)) {
            newDirection = MoveDirection.Down;
            
        } else if (Input.GetKeyDown(_leftKeyCode)) {
            newDirection = MoveDirection.Left;
            
        } else if (Input.GetKeyDown(_rightKeyCode)) {
            newDirection = MoveDirection.Right;
        }

        if (newDirection != MoveDirection.Invalid) {
            if (!Move(newDirection)){
                Debug.Log("Could not move player in direction " + newDirection.ToString());
            }
        }
    }
}
