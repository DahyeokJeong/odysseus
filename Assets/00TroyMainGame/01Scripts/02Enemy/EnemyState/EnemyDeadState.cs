using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyController controller;

    public EnemyDeadState(EnemyController controller)
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
    }

    public void Tick()
    {
    }
}
