public class BossDeadState : IState
{
    protected BossController controller;

    public BossDeadState(BossController controller)
    {
        this.controller = controller;
    }

    public virtual void Enter()
    {
        controller.Telegraph.Hide();

        controller.Death.Dead();
    }

    public virtual void Tick()
    {

    }

    public virtual void FixedTick()
    {

    }

    public virtual void Exit()
    {

    }
}