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
        controller.Attack.SetAttackDirection(controller.InputHandler.MouseWorldPos);

        Vector2 attackDirection = controller.Attack.AttackDirection;

        controller.View.UpdateFacing(controller.Attack.AttackDirection);

        controller.Attack.StartAttack();

        PlayAttackAnimation(attackDirection);
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

    private void PlayAttackAnimation(Vector2 attackDirection)
    {
        string animName = "Player_Attack";

        if (Mathf.Abs(attackDirection.y) > Mathf.Abs(attackDirection.x))
        {
            if (attackDirection.y < 0f)
                animName = "Player_Attack_Forward";
            else
                animName = "Player_Attack_Up";
        }

        controller.Animator.Play(animName, 0, 0f);
    }
}
