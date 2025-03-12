using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Villager : MonoBehaviour {

    protected NavMeshAgent _agent;
    [SerializeField] private float _minWalkDistance = 5;
    [SerializeField] private float _maxWalkDistance = 20;

    private const float IDLE_SPEED = 0f;
    private const float WALK_SPEED = 2f;
    private const float RUN_SPEED = 5f;

    public event Action<float> OnSpeedChanged;
    private float _speed = IDLE_SPEED;
    public float Speed {
        get {
            return _speed;
        }
        private set {
            _speed = value;
            _agent.speed = Speed;
            OnSpeedChanged?.Invoke(Speed);
        }
    }
    private VillagerStates _currentState = VillagerStates.Idle;
    public VillagerStates CurrentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            switch (CurrentState) {
                default:
                case VillagerStates.Idle:
                    Speed = IDLE_SPEED;
                    break;
                case VillagerStates.Walking:
                    Speed = WALK_SPEED;
                    break;
                case VillagerStates.Running:
                    Speed = RUN_SPEED;
                    break;
            }
        }
    }
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