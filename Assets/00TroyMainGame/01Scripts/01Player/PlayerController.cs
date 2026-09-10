using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IState currentState;

    private PlayerInputHandler inputHandler;
    private PlayerMovement movement;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        movement = GetComponent<PlayerMovement>();

        IdleState = new PlayerIdleState(
            this,
            inputHandler,
            movement
        );

        MoveState = new PlayerMoveState(
            this,
            inputHandler,
            movement
        );
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }
}