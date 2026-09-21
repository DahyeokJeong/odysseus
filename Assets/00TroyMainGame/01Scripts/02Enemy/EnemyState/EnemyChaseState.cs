using UnityEngine;

public class EnemyChaseState : IState
{
    private EnemyController controller;

    public EnemyChaseState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void FixedTick()
    {
        controller.Movement.Move(controller.Player.position);
    }

    public void Tick()
    {
    }
}
