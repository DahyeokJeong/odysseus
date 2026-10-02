using UnityEngine;

public class MinoSweepAttackState : IState
{
    private MinoController controller;

    private Transform attackPoint;
    private bool isHorizontal;

    private float attackTimer;
    private float telegraphDuration = 1.5f;
    private float attackDuration = 2.3f;

    private bool isAttack;

    public MinoSweepAttackState(MinoController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        attackTimer = 0f;
        isAttack = false;

        Vector2 direction = (
            controller.Target.position
            - controller.transform.position
        ).normalized;

        controller.View.LookDirection(direction);
        controller.LockDirection();

        attackPoint =
            controller.GetNormalAttackPoint();

        isHorizontal =
            controller.GetNormalAttackHorizontal();

        controller.SweepAttack.ShowTelegraph(
            controller.Telegraph,
            attackPoint,
            isHorizontal
        );
    }

    public void Tick()
    {
        attackTimer += Time.deltaTime;

        if (!isAttack &&
            attackTimer >= telegraphDuration)
        {
            controller.SweepAttack.Attack(
                attackPoint,
                isHorizontal
            );

            controller.SweepAttack.HideTelegraph(controller.Telegraph);

            isAttack = true;
        }

        if (attackTimer >= attackDuration)
        {
            controller.StartAttackCooldown();
            controller.ChangeState(
                controller.ChaseState
            );
        }
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        controller.SweepAttack.HideTelegraph(controller.Telegraph);
        controller.UnlockDirection();
    }
}