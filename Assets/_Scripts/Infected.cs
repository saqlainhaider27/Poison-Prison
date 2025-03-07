using UnityEngine;

public class Infected : Villager{
    [SerializeField] private float _searchRadius = 10f;
    [SerializeField] private LayerMask _villagerLayer;

    private enum Stops {
        Player,
        Villager
    }
    private Stops _currentStop;
    private IInfectable infectable;
    // The villager will run after the player
    private void Update() {
        if (CurrentState == VillagerStates.Idle) {
            CurrentState = VillagerStates.Running;
            // Check if the villager is close any other villager and infect them
            Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius, _villagerLayer);
            if (colliders.Length <= 0) {
                // If no villagers in search radius follow the player
                SetDestinationToPlayer();
            }
            else {
                foreach (Collider collider in colliders) {
                    if (collider.TryGetComponent<IInfectable>(out infectable)) {
                        _agent.SetDestination(collider.transform.position);
                        _currentStop = Stops.Villager;
                    }
                }
            }
        }
        if (IsStopped()) {
            CurrentState = VillagerStates.Idle;
            if (_currentStop == Stops.Villager) {
                // infect the villager
                infectable.Infect();
                infectable = null;
            }
        }
    }

    private void SetDestinationToPlayer() {
        // Get the player's position
        _currentStop = Stops.Player;
        Vector3 playerPosition = Player.Instance.transform.position;
        _agent.SetDestination(playerPosition);
    }
}