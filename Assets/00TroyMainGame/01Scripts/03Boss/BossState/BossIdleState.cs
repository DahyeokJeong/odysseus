using UnityEngine;

public class BossIdleState : IState
{
    private BossController controller;

    public BossIdleState(BossController controller)
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

        if (distance <= controller.Model.DetectRange)
        {
            controller.ChangeState(controller.ChaseState);
        }
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }
}