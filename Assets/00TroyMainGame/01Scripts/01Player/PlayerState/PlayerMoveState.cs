using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerController controller;

    public PlayerMoveState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        controller.Animator.Play("Player_Move");
    }

    public void Tick()
    {            
    }

    public void FixedTick()
    {
        controller.Movement.Move(controller.InputHandler.MoveInput);
    }

    public void Exit()
    {
    }
}