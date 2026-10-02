using UnityEngine;

public class BossChaseState : IState
{
    private BossController controller;

    public BossChaseState(BossController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {

    }

    public void Tick()
    {
        float distance = Vector2.Distance(
            controller.transform.position,
            controller.Target.position);

        if (controller.CanAttack &&
            controller.TryChasePattern())
        {
            return;
        }

        if (distance <= controller.Model.AttackRange &&
            controller.CanAttack)
        {
            controller.ChangeState(controller.AttackSelectState);
        }
    }

    public void FixedTick()
    {
        float distance = Vector2.Distance(
            controller.transform.position,
            controller.Target.position);

        if (distance <= controller.Model.AttackRange)
            return;

        Vector2 direction = (
            controller.Target.position
            - controller.transform.position
        ).normalized;

        controller.Movement.Move(direction);
    }

    public void Exit()
    {

    }
}