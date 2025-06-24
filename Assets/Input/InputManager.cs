using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static Vector2 movement;
    public static bool attack;
    public static bool dash;

    public static bool menuOpenInput;
    public static bool menuCloseInput;

    public static PlayerInput playerInput;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _dashAction;

    private InputAction _menuOpenAction;
    private InputAction _menuCloseAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _moveAction = playerInput.actions["Move"];
        _attackAction = playerInput.actions["Attack"];

        _menuOpenAction = playerInput.actions["MenuOPEN"];
        _menuCloseAction = playerInput.actions["MenuCLOSE"];
        _dashAction = playerInput.actions["Dash"];
    }

    private void Update()
    {
        movement = _moveAction.ReadValue<Vector2>();
        attack = _attackAction.WasPerformedThisFrame();
        dash = _dashAction.WasPerformedThisFrame();

        menuOpenInput = _menuOpenAction.WasPerformedThisFrame();
        menuCloseInput = _menuCloseAction.WasPerformedThisFrame();
    }

    public void ReadName(string name)
    {
        MainManager.gameManager.playerName = name;
        MainManager.menuManager.Begin();
    }
}
