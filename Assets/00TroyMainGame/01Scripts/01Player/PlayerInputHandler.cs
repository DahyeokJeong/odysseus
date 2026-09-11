using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public bool MovePressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public Vector2 MousePos { get; private set; }

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MousePos = Mouse.current.position.ReadValue();
        //Debug.Log($"Mouse Position : {mousePos}");

        MovePressed = true;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        AttackPressed = true;
    }

    private void LateUpdate()
    {
        MovePressed = false;
        AttackPressed = false;
    }
}