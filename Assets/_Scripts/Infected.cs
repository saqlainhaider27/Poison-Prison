using UnityEngine;

public class Infected : Villager{

    //private enum FollowTarget {
    //    Villager,
    //    Player
    //}
    //private FollowTarget _currentFollow = FollowTarget.Villager;

    private float _timeToNextAction;

    [SerializeField] private float _searchRadius = 500f;
    [SerializeField] private LayerMask _searchLayer;

    private IInfectable _target;
    private float _previousDistance = 10000f;
    private float _waitDuration = 3f;

    // The villager will run after the player
    private void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRadius, _searchLayer);
        foreach (Collider collider in colliders) {
            // Check the infectable closest to the infected
            if (collider.TryGetComponent<IInfectable>(out IInfectable infectable)) {
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < _previousDistance) {
                    _previousDistance = distanceToTarget;
                    _target = infectable;
                    _agent.SetDestination(collider.transform.position);
                }
            }
        }

        if (IsStopped()) {
            CurrentState = VillagerStates.Idle;
            if (_target == null) {
                return;
            }
            else {
                _target.Infect();
                _target = null;
            }
        }
        else {
            CurrentState = VillagerStates.Running;
        }

    }
}