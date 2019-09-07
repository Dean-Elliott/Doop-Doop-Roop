using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemyMover : AudioBasedMover
{
    protected override void OnBeatWindowStarted() {
        base.OnBeatWindowStarted();

        MoveDirection randomDirection = (MoveDirection)Random.Range(0, 4);

        Move(randomDirection);
    }
}
