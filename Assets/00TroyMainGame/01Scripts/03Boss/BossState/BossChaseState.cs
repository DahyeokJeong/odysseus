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

        if (distance <= controller.Model.AttackRange)
        {
            controller.ChangeState(controller.AttackSelectState);
        }
    }

    public void FixedTick()
    {
        Vector2 direction = (
            controller.Target.position - controller.transform.position
        ).normalized;

        Vector2 nextPos =
            controller.Rb.position
          + direction * controller.Model.MoveSpeed * Time.fixedDeltaTime;

        controller.Rb.MovePosition(nextPos);
    }

    public void Exit()
    {

    }
}