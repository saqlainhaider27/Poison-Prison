using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Villager : MonoBehaviour {

    protected NavMeshAgent _agent;
    private float _minWalkDistance = 5;
    private float _maxWalkDistance = 20;

    public float Speed {
        get;
        private set;
    } = 2f;
    public VillagerStates CurrentState {
        get;
        set;
    } = VillagerStates.Idle;
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Speed;
    }
    public void SetRandomDestination() {
        float distance = UnityEngine.Random.Range(_minWalkDistance, _maxWalkDistance);
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas);
        Vector3 targetPosition = hit.position;

        _agent.SetDestination(targetPosition);
    }

    public bool IsStopped() {
        if (_agent.remainingDistance <= _agent.stoppingDistance) {
            // Villager has reached the destination
            // Stop the agent
            _agent.isStopped = true;
        }
        else {
            _agent.isStopped = false;
        }
        return _agent.isStopped;

    }
}