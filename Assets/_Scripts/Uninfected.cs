using UnityEngine;

public class Uninfected : Villager, IInfectable {
    private const float MOVE_DELAY = 3f;
    private float _timeToNextAction;
    [SerializeField] private GameObject _infectedNPCPrefab;
    private void Update() {
       
        HandleIdleAndWalkingTransition(MOVE_DELAY);
    }


    public void HandleIdleAndWalkingTransition(float waitDuration) {
        if (IsStopped() && CurrentState != VillagerStates.Idle) {
            CurrentState = VillagerStates.Idle;
            _timeToNextAction = Time.time + waitDuration;
        }

        if (IsStopped() && Time.time >= _timeToNextAction) {
            CurrentState = VillagerStates.Walking;
            SetRandomDestination();
        }
    }

    public void Infect() {

        // TODO:
        // Destroy the uninfected villager
        // Spawn an infected
        GameObject gameObject = Instantiate(_infectedNPCPrefab,transform.position, transform.rotation);
        Destroy(this.gameObject);

    }
}