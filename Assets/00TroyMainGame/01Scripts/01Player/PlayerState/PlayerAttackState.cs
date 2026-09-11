using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController controller;

    public PlayerAttackState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        Debug.Log("Player - Enter Attack State");

        controller.Animator.Play("Player_Attack");
    }

    public void Tick()
    {
        
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        Debug.Log("Player - Exit Attack State");
    }
}
