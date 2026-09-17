using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController controller;

    public PlayerIdleState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.Animator.Play("Player_Idle");
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
