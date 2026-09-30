using UnityEngine;

public class BossNormalAttackState : IState
{
    private BossController controller;

    private float attackTimer;
    private float telegraphDuration = 0.5f;
    private float attackDuration = 1f;

    private bool isAttack;

    public BossNormalAttackState(BossController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        attackTimer = 0f;
        isAttack = false;

        ShowTelegraph();
    }

    public void Tick()
    {
        attackTimer += Time.deltaTime;

        if (!isAttack && attackTimer >= telegraphDuration)
        {
            Attack();
            HideTelegraph();

            isAttack = true;
        }

        if (attackTimer >= attackDuration)
        {
            controller.ChangeState(controller.ChaseState);
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

    }

    private void HideTelegraph()
    {

    }

    private void Attack()
    {

    }
}