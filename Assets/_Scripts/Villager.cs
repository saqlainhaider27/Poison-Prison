using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Villager : MonoBehaviour {

    protected NavMeshAgent _agent;
    [SerializeField] private float _minWalkDistance = 5;
    [SerializeField] private float _maxWalkDistance = 20;

    public VillagerStates CurrentState {
        get;
        set;
    } = VillagerStates.Idle;
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
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
            return true;
        }
        else {
            return false;
        }
    }
}