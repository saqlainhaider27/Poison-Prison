using System;
using UnityEngine;

public class Uninfected : Villager {
    private const float MOVE_DELAY = 3f;
    private float timeToNextAction;
    private void Update() {
       
        HandleIdleAndWalkingTransition(MOVE_DELAY);
    }


    public void HandleIdleAndWalkingTransition(float waitDuration) {
        if (IsStopped() && CurrentState != VillagerStates.Idle) {
            CurrentState = VillagerStates.Idle;
            timeToNextAction = Time.time + waitDuration;
        }

        if (IsStopped() && Time.time >= timeToNextAction) {
            CurrentState = VillagerStates.Walking;
            SetRandomDestination();
        }
    }

    public void Infect() {

        // TODO:
        // Destroy the uninfected villager
        // Spawn an infected

    }
}