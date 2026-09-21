using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyModel model;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyAttack attack;

    [Header("Detection")]
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Target")]
    [SerializeField] private Transform player;

    public Vector2 SpawnPos {  get; private set; }

    public EnemyModel Model => model;
    public Animator Animator => animator; // 읽기 전용 프로퍼티
    public Transform Player => player;
    public float DetectRange => detectRange;
    public float ChaseRange => chaseRange;
    public float AttackRange => attackRange;
    public EnemyMovement Movement => movement;
    public EnemyAttack Attack => attack;

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

        SpawnPos = transform.position;
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
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        float spawnDistance = Vector2.Distance(SpawnPos, player.position);
        
        if (currentState == idleState)
        {
            if (distance <= detectRange)
                ChangeState(chaseState);
        }

        else if (currentState == chaseState)
        {
            if (spawnDistance > chaseRange)
            {
                ChangeState(returnState);
            }
            else if (distance <= attackRange)
            {
                ChangeState(attackState);
            }
        }
        
        else if (currentState == attackState)
        {
            if (spawnDistance > chaseRange)
            {
                ChangeState(returnState);
            }
            else if (distance > attackRange)
            {
                ChangeState(chaseState);
            }
        }
        
        else if (currentState == returnState)
        {
            float returnDistance = Vector2.Distance(
                transform.position,
                SpawnPos
            );

            if (returnDistance <= 0.05f)
                ChangeState(idleState);
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        prevState = currentState;
        currentState = newState;

        currentState?.Enter();
    }

    private void OnDrawGizmosSelected()
    {
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();

        if (col == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }
}
