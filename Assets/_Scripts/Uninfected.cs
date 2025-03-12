using UnityEngine;

public class Uninfected : Villager, IInfectable {

    private const float MOVE_DELAY = 3f;
    private float _timeToNextAction;

    [SerializeField] private GameObject _infectedNPCPrefab;
    private void Update() {       
        HandleIdleAndWalkingTransition(MOVE_DELAY);
    }
    public void HandleIdleAndWalkingTransition(float waitDuration) {
        if (IsStopped()) {
            CurrentState = VillagerStates.Idle;
        }
        else {
            CurrentState = VillagerStates.Walking;
        }

        if (Time.time >= _timeToNextAction) {
            SetRandomDestination();
            _timeToNextAction = Time.time + waitDuration;
        }

    }

    public void Infect() {
        Instantiate(_infectedNPCPrefab,transform.position, transform.rotation);
        Destroy(this.gameObject);                                                                                       
    }
}