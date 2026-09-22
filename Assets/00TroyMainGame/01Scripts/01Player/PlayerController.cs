using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerView view;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private PlayerHit hit;
    [SerializeField] public Animator animator;

    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerModel Model => model;
    public PlayerView View => view;
    public PlayerMovement Movement => movement;
    public PlayerAttack Attack => attack;
    public PlayerHit Hit => hit;
    public Animator Animator => animator;

    private IState currentState;
    private IState prevState;

    private PlayerIdleState idleState;
    private PlayerMoveState moveState;
    private PlayerAttackState attackState;
    private PlayerHitState hitState;

    private void OnDestroy()
    {
        if (hit != null)
            hit.OnHit -= HandleHit;
    }

    private void Awake()
    {        
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        hitState = new PlayerHitState(this);

        hit.OnHit += HandleHit;
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        HandleStateChange();

        if (currentState != attackState && currentState != hitState)
            view.UpdateFacing(inputHandler.MoveInput);

        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    private void HandleStateChange()
    {
        if (currentState == hitState)
        {
            if (!hit.IsHitFinished)
                return;

            ChangeState(idleState);
            return;
        }

        if (currentState == attackState)
        {
            if (!attack.IsAttackFinished)
                return;

            ChangeState(idleState);
            return;
        }

        if (inputHandler.AttackPressed && attack.CanAttack)
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

    private void HandleHit()
    {
        ChangeState(hitState);
    }
}