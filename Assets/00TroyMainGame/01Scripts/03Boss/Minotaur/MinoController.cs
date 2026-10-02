using UnityEngine;

public enum MinoPhase
{
    Phase1,
    Phase2,
    Phase3
}

public class MinoController : BossController
{
    [Header("Component")]
    [SerializeField] private MinoView view;

    [Header("Dash")]
    [SerializeField] private MinoDashAttack dashAttack;
    [SerializeField] private float dashRange = 7f;
    [SerializeField] private float dashCheckCooldown = 5f;

    [Header("Sweep")]
    [SerializeField] private MinoSweepAttack sweepAttack;

    [Header("Death")]
    [SerializeField] private MinoDeath minoDeath;

    private MinoDashAttackState dashAttackState;
    private MinoSweepAttackState sweepAttackState;
    private MinoStunState stunState;
    private MinoDeadState deadState;

    private MinoPhase currentPhase;

    private float dashCheckTimer;
    private bool isDirectionLocked;

    public MinoView View => view;
    public MinoDashAttack DashAttack => dashAttack;
    public MinoSweepAttack SweepAttack => sweepAttack;
    public MinoDeath MinoDeath => minoDeath;
    public MinoPhase CurrentPhase => currentPhase;

    public MinoStunState StunState => stunState;

    public float DashRange => dashRange;
    public bool CanCheckDash => dashCheckTimer <= 0f;
    public bool IsDirectionLocked => isDirectionLocked;

    protected override void Awake()
    {
        base.Awake();

        dashAttackState = new MinoDashAttackState(this);
        sweepAttackState = new MinoSweepAttackState(this);
        stunState = new MinoStunState(this);
        deadState = new MinoDeadState(this);

        currentPhase = MinoPhase.Phase1;

        Model.OnHPChanged += CheckPhase;
        Model.OnDead += HandleDead;
    }

    protected override void Update()
    {
        base.Update();

        if (dashCheckTimer > 0f)
            dashCheckTimer -= Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isDirectionLocked)
            return;

        Vector2 direction = (
            Target.position - transform.position
        ).normalized;

        View.LookDirection(direction);
    }

    public override Transform GetNormalAttackPoint()
    {
        return View.CurrentAttackPoint;
    }

    public override bool GetNormalAttackHorizontal()
    {
        return View.IsHorizontal;
    }

    public override void SelectPattern()
    {
        int pattern = attackSelectState.SelectAttack();

        switch (pattern)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                ChangeState(normalAttackState);
                break;

            case 4:
                ChangeState(dashAttackState);
                break;

            case 5:
                ChangeState(sweepAttackState);
                break;
        }
    }

    public override bool TryChasePattern()
    {
        float distance = Vector2.Distance(
            transform.position,
            Target.position
        );

        if (distance > dashRange || !CanCheckDash)
            return false;

        StartDashCheckCooldown();

        int pattern = attackSelectState.SelectAttack();

        if (pattern != 4)
            return false;

        ChangeState(dashAttackState);

        return true;
    }

    public void LockDirection()
    {
        isDirectionLocked = true;
    }

    public void UnlockDirection()
    {
        isDirectionLocked = false;
    }

    public void StartDashCheckCooldown()
    {
        dashCheckTimer = dashCheckCooldown;
    }

    private void CheckPhase()
    {
        float hpRatio =
            Model.CurrentHP / Model.MaxHP;

        if (hpRatio <= 0.25f)
        {
            ChangePhase(MinoPhase.Phase3);
        }
        else if (hpRatio <= 0.5f)
        {
            ChangePhase(MinoPhase.Phase2);
        }
        else
        {
            ChangePhase(MinoPhase.Phase1);
        }
    }

    private void ChangePhase(MinoPhase newPhase)
    {
        if (currentPhase == newPhase)
            return;

        currentPhase = newPhase;

        switch (currentPhase)
        {
            case MinoPhase.Phase1:
                DashAttack.SetPhaseMultiplier(1f);
                break;

            case MinoPhase.Phase2:
                DashAttack.SetPhaseMultiplier(1.25f);
                break;

            case MinoPhase.Phase3:
                DashAttack.SetPhaseMultiplier(1.5f);
                break;
        }

        Debug.Log($"Minotaur Phase : {currentPhase}");
    }

    private void HandleDead()
    {
        ChangeState(deadState);
    }

    private void OnDestroy()
    {
        Model.OnHPChanged -= CheckPhase;
        Model.OnDead -= HandleDead;
    }
}