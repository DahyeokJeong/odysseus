using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseWorldPos { get; private set; }
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

    private void Update()
    {
        UpdateMousePos();
    }

    private void LateUpdate()
    {
        AttackPressed = false;
    }

    private void UpdateMousePos()
    {
        if (Mouse.current == null)
            return;

        Camera mainCam = Camera.main;

        if (mainCam == null)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(
                                                                mouseScreenPos.x,
                                                                mouseScreenPos.y,
                                                                -mainCam.transform.position.z));

        MouseWorldPos = mouseWorldPos;

        //Debug.Log($"Mouse World Pos : {MouseWorldPos}");
    }
}