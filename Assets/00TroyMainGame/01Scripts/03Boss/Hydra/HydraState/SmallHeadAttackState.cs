using UnityEngine;

public class SmallHeadAttackState : IState
{
    private HydraHeadController controller;

    private float attackTimer;
    private float attackDuration = 2f;

    public SmallHeadAttackState(HydraHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        attackTimer = 0f;

        if (controller.PoisonAttack != null)
        {
            controller.PoisonAttack.StartAttack();
        }
    }

    public void Tick()
    {
        if (controller.PoisonAttack != null)
        {
            controller.PoisonAttack.Tick();

            if (controller.PoisonAttack.IsAttackFinished)
            {
                controller.ChangeState(controller.IdleState);
            }

            return;
        }

        // 공격이 구현되지 않은 나머지 머리
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDuration)
        {
            controller.ChangeState(controller.IdleState);
        }
    }


    public void FixedTick()
    {

    }

    public void Exit()
    {
        controller.HydraController.EndSmallHeadAttack();
    }
}