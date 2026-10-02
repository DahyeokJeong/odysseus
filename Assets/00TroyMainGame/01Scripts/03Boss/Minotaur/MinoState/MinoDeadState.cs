public class MinoDeadState : BossDeadState
{
    private MinoController minoController;

    public MinoDeadState(MinoController controller)
        : base(controller)
    {
        minoController = controller;
    }

    public override void Enter()
    {
        base.Enter();

        minoController.LockDirection();
        minoController.MinoDeath.Dead();
    }
}