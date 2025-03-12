using System;
using UnityEngine;

public class Player : Singleton<Player> {

    private const float IDLE_SPEED = 0f;
    private const float RUN_SPEED = 5f;

    public event Action<float> OnSpeedChanged;

    private float _speed;
    public float Speed {
        get {
            return _speed;
        }
        private set {
            _speed = value;
            OnSpeedChanged?.Invoke(value);
        }
    }

    
    public float Health {
        get;
        private set; 
    } = 100f;

    private PlayerStates _currentState;
    public PlayerStates CurrentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            switch (CurrentState) {
                default:
                case PlayerStates.Idle:
                    Speed = IDLE_SPEED;
                    break;
                case PlayerStates.Running:
                    Speed = RUN_SPEED;
                    break;
                case PlayerStates.Falling:
                    break;
            }
        }
    }
}