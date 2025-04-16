using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static Vector2 _movement;
    public static float _attack;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _attackAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _attackAction = _playerInput.actions["Attack"];
    }

    private void Update()
    {
        _movement = _moveAction.ReadValue<Vector2>();
        _attack = _attackAction.ReadValue<float>();
    }
}
