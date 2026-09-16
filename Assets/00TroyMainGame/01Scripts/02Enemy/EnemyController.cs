using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyModel model;
    [SerializeField] private Animator animator;

    public EnemyModel Model => model;
    public Animator Animator => animator; // 읽기 전용 프로퍼티

    private IState currentState;
    private IState prevState;

    private EnemyIdleState idleState;
    private EnemyChaseState chaseState;
    private EnemyAttackState attackState;
    private EnemyHitState hitState;
    private EnemyDeadState deadState;
    private EnemyReturnState returnState;

    private void Awake()
    {
        idleState = new EnemyIdleState(this);
        chaseState = new EnemyChaseState(this);
        attackState = new EnemyAttackState(this);
        hitState = new EnemyHitState(this);
        deadState = new EnemyDeadState(this);
        returnState = new EnemyReturnState(this);
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

    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        prevState = currentState;
        currentState = newState;

        currentState?.Enter();
    }
}
