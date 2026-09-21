using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerController controller;

    private string currentAnim;

    public PlayerMoveState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        currentAnim = "";
    }

    public void Tick()
    {
        Vector2 moveInput = controller.InputHandler.MoveInput;

        controller.View.UpdateFacing(moveInput);

        UpdateAnimation(moveInput);
    }

    public void FixedTick()
    {
        controller.Movement.Move(controller.InputHandler.MoveInput);
    }

    public void Exit()
    {
    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        if (moveInput.sqrMagnitude < 0.001f)
            return;

        string nextAnim = "Player_Move";

        // 아래쪽 이동
        if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
        {
            if (moveInput.y < 0f)
                nextAnim = "Player_Move_Forward";

            else
                nextAnim = "Player_Move_Up";
        }

        // 애니메이션이 바뀔 때만 재생
        if (currentAnim != nextAnim)
        {
            controller.Animator.Play(nextAnim);

            currentAnim = nextAnim;
        }
    }
}