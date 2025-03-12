using System;
using UnityEngine;

public class InputController : Singleton<InputController>{
    
    private InputActions _inputActions;

    private void Awake() {
        _inputActions = new InputActions();
    }
    public Vector2 GetInputVector() {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }
    public Vector2 GetInputVectorNormalized() {
        return _inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
    public bool IsJumpPressed() {
        return _inputActions.Player.Jump.triggered;
    }
    public bool IsAttackPressed() {
        return _inputActions.Player.Attack.triggered;
    }
    public bool IsShiftPressed() {
        return _inputActions.Player.Sprint.triggered;
    }
    private void OnEnable() {
        _inputActions.Enable();
    }
    private void OnDisable() {
        _inputActions.Disable();
    }
}