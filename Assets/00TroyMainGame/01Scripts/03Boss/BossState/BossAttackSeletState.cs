using UnityEngine;

public class BossAttackSelectState : IState
{
    private BossController controller;

    public BossAttackSelectState(BossController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.SelectPattern();
    }

    public void Tick()
    {

    }

    public void FixedTick()
    {

    }

    public void Exit()
    {

    }

    public int SelectAttack()
    {
        return Random.Range(0, 3);
    }
}