using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BossModel model;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BossMovement movement;
    [SerializeField] private BossDeath death;
    [SerializeField] private BossNormalAttack normalAttack;
    [SerializeField] private BossTelegraph telegraphPrefab;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 2.5f;

    private float attackCooldownTimer;
    public bool CanAttack => attackCooldownTimer <= 0f;

    protected IState currentState;

    protected BossIdleState idleState;
    protected BossChaseState chaseState;
    protected BossAttackSelectState attackSelectState;
    protected BossNormalAttackState normalAttackState;

    private BossTelegraph telegraph;

    public BossModel Model => model;
    public Rigidbody2D Rb => rb;
    public BossMovement Movement => movement;
    public BossDeath Death => death;
    public Transform Target => target;
    public BossNormalAttack NormalAttack => normalAttack;
    public BossTelegraph Telegraph => telegraph;

    public BossIdleState IdleState => idleState;
    public BossChaseState ChaseState => chaseState;
    public BossAttackSelectState AttackSelectState => attackSelectState;
    public BossNormalAttackState NormalAttackState => normalAttackState;

    protected virtual void Awake()
    {
        telegraph = Instantiate(
            telegraphPrefab,
            transform
        );

        telegraph.Hide();

        idleState = new BossIdleState(this);
        chaseState = new BossChaseState(this);
        attackSelectState = new BossAttackSelectState(this);
        normalAttackState = new BossNormalAttackState(this);
    }

    protected virtual void Start()
    {
        ChangeState(idleState);
    }

    protected virtual void Update()
    {
        if (attackCooldownTimer > 0f)
            attackCooldownTimer -= Time.deltaTime;

        currentState?.Tick();
    }

    protected virtual void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    public virtual Transform GetNormalAttackPoint()
    {
        return null;
    }

    public virtual bool GetNormalAttackHorizontal()
    {
        return true;
    }

    public void StartAttackCooldown()
    {
        attackCooldownTimer = attackCooldown;
    }

    public virtual void SelectPattern()
    {

    }

    public virtual bool TryChasePattern()
    {
        return false;
    }

    public void ChangeState(IState newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }
}