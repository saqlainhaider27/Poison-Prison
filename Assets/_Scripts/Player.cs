using System;

public class Player : Singleton<Player>, IInfectable {

    private const float IDLE_SPEED = 0f;
    private const float RUN_SPEED = 7f;

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
    } = 3f;

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
                case PlayerStates.Died:
                    break;
            }
        }
    }

    public void Infect() {
        if (Health <= 0) {
            return;
        }
        Health -= 1;
        if (Health == 0) {
            Die();
        }
    }

    private void Die() {
        CurrentState = PlayerStates.Died;
    }
}