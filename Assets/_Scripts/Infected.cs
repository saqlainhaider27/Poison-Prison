using UnityEngine;

public class Infected : Villager{

    [SerializeField] private float _searchRadius = 10f;
    [SerializeField] private LayerMask _villagerLayer;

    // The villager will run after the player
    private void Update() {
        if (IsStopped()) {
            CurrentState = VillagerStates.Idle;
        }
        else {
            CurrentState = VillagerStates.Running;
        }

        // Check if the villager is close any other villager and infect them
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius, _villagerLayer);
        if (colliders.Length <= 0) {
            // If no villagers in search radius follow the player
            SetDestinationToPlayer();
        }
        else {
            foreach (Collider collider in colliders) {
                if (collider.TryGetComponent<IInfectable>(out IInfectable infectable)) {
                    _agent.SetDestination(collider.transform.position);
                    if (IsStopped()) {
                        infectable.Infect();
                    }
                }
            }
        }
    }

    private void SetDestinationToPlayer() {
        // Get the player's position
        Vector3 playerPosition = Player.Instance.transform.position;
        _agent.SetDestination(playerPosition);
    }
}