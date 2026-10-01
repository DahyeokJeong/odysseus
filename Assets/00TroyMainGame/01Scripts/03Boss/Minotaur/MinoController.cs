using UnityEngine;

public class MinoController : BossController
{
    [Header("Component")]
    [SerializeField] private MinoView view;
    [SerializeField] private MinoDashAttack dashAttack;

    [Header("Dash")]
    [SerializeField] private float dashRange = 7f;
    [SerializeField] private float dashCheckCooldown = 5f;

    private float dashCheckTimer;

    public float DashRange => dashRange;
    public bool CanCheckDash => dashCheckTimer <= 0f;

    private MinoDashAttackState dashAttackState;

    public MinoView View => view;
    public MinoDashAttack DashAttack => dashAttack;

    private bool isDirectionLocked;

    public bool IsDirectionLocked => isDirectionLocked;

    protected override void Awake()
    {
        base.Awake();

        dashAttackState = new MinoDashAttackState(this);
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
                //SweepState
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
}
