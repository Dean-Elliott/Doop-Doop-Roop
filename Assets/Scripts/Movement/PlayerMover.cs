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
        if (Input.GetKeyDown(_upKeyCode)) {
            Move(MoveDirection.Up);

        } else if (Input.GetKeyDown(_downKeyCode)) {
            Move(MoveDirection.Down);

        } else if (Input.GetKeyDown(_leftKeyCode)) {
            Move(MoveDirection.Left);

        } else if (Input.GetKeyDown(_rightKeyCode)) {
            Move(MoveDirection.Right);
        }
    }
}
