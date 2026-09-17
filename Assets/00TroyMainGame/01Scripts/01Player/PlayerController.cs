using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerView view;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] public Animator animator;

    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerMovement Movement => movement;
    public PlayerAttack Attack => attack;
    public Animator Animator => animator;

    private IState currentState;
    private IState prevState;

    private PlayerIdleState idleState;
    private PlayerMoveState moveState;
    private PlayerAttackState attackState;

    private void Awake()
    {        
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

        if (currentState != attackState)
            view.UpdateFacing(inputHandler.MoveInput);

        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    private void HandleStateChange()
    {
        if (currentState == attackState)
        {
            if (!attack.IsAttackFinished)
                return;

            ChangeState(idleState);
            return;
        }

        if (inputHandler.AttackPressed)
        {
            ChangeState(attackState);
            return;
        }

        if (inputHandler.MoveInput.sqrMagnitude > 0f)
        {
            if (currentState != moveState)
                ChangeState(moveState);

            return;
        }

        if (currentState == moveState)
        {
            ChangeState(idleState);
            return;
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        prevState = currentState;
        currentState = newState;

        currentState?.Enter();
    }
}