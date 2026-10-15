public class MainHeadPoisonPoolState : IState
{
    private MainHeadController controller;

    public MainHeadPoisonPoolState(MainHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.PoisonPoolAttack.StartAttack();
    }

    public void Tick()
    {
        controller.PoisonPoolAttack.Tick();

        if (controller.PoisonPoolAttack.IsAttackFinished)
            controller.ChangeState(controller.IdleState);
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
        controller.PoisonPoolAttack.StopAttack();
        controller.HydraController.EndMainHeadAttack();
    }
}