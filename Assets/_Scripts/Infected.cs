using System;
using UnityEngine;

public class Infected : Villager{
    private float _searchRadius = 10f;

    // The villager will run after the player
    private void Update() {
        if (CurrentState == VillagerStates.Idle) {
            CurrentState = VillagerStates.Running;
            // Check if the villager is close any other villager and infect them
            Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius);
            foreach (Collider collider in colliders) {
                if ( collider.TryGetComponent<Uninfected>(out Uninfected component)) {
                    // there is a villager in search radius
                    // abandon the player and infect the villager
                    component.Infect();
                }
            }
            SetDestinationToPlayer();
        }
    }

    private void SetDestinationToPlayer() {
        // Get the player's position
        Vector3 playerPosition = Player.Instance.transform.position;
        _agent.SetDestination(playerPosition);
    }
}