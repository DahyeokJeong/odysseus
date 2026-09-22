using UnityEngine;

public class PlayerHitState : IState
{
    private PlayerController controller;

    public PlayerHitState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.Hit.StartHit();
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
}
