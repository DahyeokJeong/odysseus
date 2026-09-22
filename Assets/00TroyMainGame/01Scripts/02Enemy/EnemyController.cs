using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyModel model;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private EnemyHit hit;
    [SerializeField] private Animator animator;

    [Header("Target")]
    [SerializeField] private Transform player;

    public Vector2 SpawnPos {  get; private set; }

    public EnemyModel Model => model;
    public EnemyMovement Movement => movement;
    public EnemyAttack Attack => attack;
    public EnemyHit Hit => hit;
    public Animator Animator => animator;

    public Transform Player => player;

    public float DetectRange => model.DetectRange;
    public float ChaseRange => model.ChaseRange;
    public float AttackRange => model.AttackRange;

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

        hit.OnHit += HandleHit;
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
        // 사망 상태
        if (currentState == deadState)
            return;

        if (model.CurrentHP <= 0f)
        {
            ChangeState(deadState);
            return;
        }

        // 피격 상태
        if (currentState == hitState)
        {
            if (!hit.IsHitFinished)
                return;

            ChangeState(idleState);
            return;
        }

        if (player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        float spawnDistance = Vector2.Distance(
            SpawnPos,
            player.position
        );

        if (currentState == idleState)
        {
            if (distance <= DetectRange)
                ChangeState(chaseState);
        }

        else if (currentState == chaseState)
        {
            if (spawnDistance > ChaseRange)
            {
                ChangeState(returnState);
            }
            else if (distance <= AttackRange)
            {
                ChangeState(attackState);
            }
        }

        else if (currentState == attackState)
        {
            if (spawnDistance > ChaseRange)
            {
                ChangeState(returnState);
            }
            else if (distance > AttackRange)
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
        if (currentState == newState)
            return;

        //Debug.Log($"{name} : {currentState?.GetType().Name} → {newState.GetType().Name}");

        currentState?.Exit();

        prevState = currentState;
        currentState = newState;

        currentState?.Enter();
    }

    private void HandleHit()
    {
        if (model.CurrentHP <= 0f)
        {
            ChangeState(deadState);
            return;
        }

        ChangeState(hitState);
    }

    private void OnDestroy()
    {
        if (hit != null)
            hit.OnHit -= HandleHit;
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
