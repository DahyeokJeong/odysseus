using UnityEngine;

public class BossNormalAttackState : IState
{
    private BossController controller;

    private Transform attackPoint;
    private bool isHorizontal;

    private float attackTimer;
    private float telegraphDuration = 1.2f;
    private float attackDuration = 2f;

    private bool isAttack;

    public BossNormalAttackState(BossController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        attackTimer = 0f;
        isAttack = false;

        attackPoint = controller.GetNormalAttackPoint();
        isHorizontal = controller.GetNormalAttackHorizontal();

        ShowTelegraph();
    }

    public void Tick()
    {
        attackTimer += Time.deltaTime;

        if (!isAttack && attackTimer >= telegraphDuration)
        {
            controller.NormalAttack.Attack(
                attackPoint,
                isHorizontal
            );

            HideTelegraph();

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
        HideTelegraph();
    }

    private void ShowTelegraph()
    {
        controller.NormalAttack.ShowTelegraph(
            controller.Telegraph,
            attackPoint,
            isHorizontal
        );
    }

    private void HideTelegraph()
    {
        controller.NormalAttack.HideTelegraph(
            controller.Telegraph
        );
    }
}