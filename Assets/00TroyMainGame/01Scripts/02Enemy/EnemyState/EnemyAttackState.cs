using UnityEngine;

public class EnemyAttackState : IState
{
    private EnemyController controller;

    public EnemyAttackState(EnemyController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
    }

    public void Tick()
    {
        controller.Attack.TryAttack();
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
    }
}
