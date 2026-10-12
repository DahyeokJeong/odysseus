public class SmallHeadIdleState : IState
{
    private HydraHeadController controller;

    public SmallHeadIdleState(HydraHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.View.PlayIdle();
    }

    public void Tick()
    {

    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        controller.View.StopIdle();
    }
}