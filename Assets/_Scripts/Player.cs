using UnityEngine;

public class Player : Singleton<Player> {

    public float Speed {
        get;
        private set;
    } = 5f;
    public float Health {
        get;
        private set; 
    } = 100f;
    public PlayerStates CurrentState {
        get;
        set;
    } = PlayerStates.Idle;
}