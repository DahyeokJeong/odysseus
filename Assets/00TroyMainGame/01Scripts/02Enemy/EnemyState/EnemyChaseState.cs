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
    }

    public void Tick()
    {
    }
}
