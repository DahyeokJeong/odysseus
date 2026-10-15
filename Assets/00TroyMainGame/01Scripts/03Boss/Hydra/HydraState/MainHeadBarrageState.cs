
public class MainHeadBarrageState : IState
{
    private MainHeadController controller;

    public MainHeadBarrageState(MainHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.Barrage.StartAttack();
    }

    public void Tick()
    {
        controller.Barrage.Tick();

        if (controller.Barrage.IsAttackFinished)
            controller.ChangeState(controller.IdleState);
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
        controller.Barrage.StopAttack();
        controller.HydraController.EndMainHeadAttack();
    }
}
