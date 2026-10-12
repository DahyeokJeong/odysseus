public class MainHeadBreathState : IState
{
    private MainHeadController controller;

    public MainHeadBreathState(MainHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.Breath.StartBreath();
    }

    public void Tick()
    {
        controller.Breath.Tick();

        if (controller.Breath.IsBreathFinished)
        {
            controller.ChangeState(controller.IdleState);
        }
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
        controller.Breath.StopBreath();
        controller.HydraController.EndMainHeadBreath();
    }
}