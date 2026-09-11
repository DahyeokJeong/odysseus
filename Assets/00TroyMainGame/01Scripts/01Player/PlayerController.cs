using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] public Animator animator;

    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerMovement Movement => movement;
    public PlayerAttack Attack => attack;
    public Animator Animator => animator;

    private Camera mainCam;

    private IState currentState;

    private PlayerIdleState idleState;
    private PlayerMoveState moveState;
    private PlayerAttackState attackState;

    private void Awake()
    {
        mainCam = Camera.main;
        
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        HandleStateChange();

        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    private void HandleStateChange()
    {
        if (inputHandler.MovePressed)
        {
            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(inputHandler.MousePos);

            mouseWorldPos.z = 0f;

            if (movement.TrySetTarget(mouseWorldPos))
                ChangeState(moveState);

            return;
        }

        if (inputHandler.AttackPressed)
        {
            ChangeState(attackState);
            return;
        }

        if (currentState == moveState && !movement.IsMoving)
        {
            ChangeState(idleState);
            return;
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }
}