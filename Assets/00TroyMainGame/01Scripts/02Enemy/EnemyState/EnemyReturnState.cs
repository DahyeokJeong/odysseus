using UnityEngine;

public class EnemyReturnState : IState
{
    private EnemyController controller;

    public EnemyReturnState(EnemyController controller)
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
        controller.Movement.Move(controller.SpawnPos);
    }

    public void Tick()
    {
    }
}
