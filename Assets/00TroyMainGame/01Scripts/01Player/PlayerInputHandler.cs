using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }
    public bool AttackPressed { get; private set; }

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Attack.performed += OnAttack;

        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Attack.performed -= OnAttack;

        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        AttackPressed = true;
    }

    private void LateUpdate()
    {
        AttackPressed = false;
    }
}