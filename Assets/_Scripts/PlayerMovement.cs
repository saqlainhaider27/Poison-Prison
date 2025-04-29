using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    private Player _player;
    private CharacterController _characterController;
    private float _currentVelocity;
    private const float GRAVITY = 9.81f;

    private void Awake() {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update() {
        if (_player.CurrentState == PlayerStates.Died) {
            return;
        }
        AddGravity();
        Vector2 inputVector = InputController.Instance.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        if (inputVector != Vector2.zero) {
            // Player is moving
            float speed = _player.Speed;
            Move(moveDirection, speed);
            // Orient the player in the direction of movement
            OrientBody(moveDirection);
        }
        else {
            // Player is idle
            _player.CurrentState = PlayerStates.Idle;
        }
    }

    private void OrientBody(Vector3 moveDir) {
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        const float SMOOTH_TIME = 0.1f;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, SMOOTH_TIME);
        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
    }

    private void AddGravity() {
        float verticalVelocity = 0f;
        if (!_characterController.isGrounded) {
            verticalVelocity -= GRAVITY * Time.deltaTime;
        }
        else {
            verticalVelocity = 0;
        }
        // Move the player down
        _player.CurrentState = PlayerStates.Falling;
        _characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void Move(Vector3 moveDir, float speed) {
        // moveDir = transform.TransformDirection(moveDir);
        _characterController.Move(moveDir * Time.deltaTime * speed);
        _player.CurrentState = PlayerStates.Running;
    }
}
