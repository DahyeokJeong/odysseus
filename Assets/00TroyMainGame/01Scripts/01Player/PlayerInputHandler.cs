using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public bool MovePressed { get; private set; }
    public Vector2 MousePos { get; private set; }

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void LateUpdate()
    {
        MovePressed = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MousePos = Mouse.current.position.ReadValue();
        //Debug.Log($"Mouse Position : {mousePos}");

        MovePressed = true;
    }
}