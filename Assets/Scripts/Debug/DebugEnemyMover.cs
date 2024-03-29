﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------
// Name: DebugEnemyMover
// Desc: Small class that randomly moves enemies in a direction by BPM
//---------------------------------------------------------------------

public class DebugEnemyMover : AudioBasedMover
{
    protected override void OnBeatWindowStarted() {
        base.OnBeatWindowStarted();

        MoveDirection randomDirection = MoveDirection.Invalid;

        // Keep trying directions with valid movements
        do {
            randomDirection = (MoveDirection)Random.Range(0, 4);

        } while (!Move(randomDirection));
    }
}
