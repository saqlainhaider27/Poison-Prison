using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private const string SPEED = "Speed";
    private Animator _anim;
    private Player _player;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();
    }
    private void Start() {
        _player.OnSpeedChanged += Player_OnSpeedChanged;
    }

    private void Player_OnSpeedChanged(float speed) {
        _anim.SetFloat(SPEED, speed, 0.1f, Time.deltaTime);
    }
}
