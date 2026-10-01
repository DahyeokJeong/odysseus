using UnityEngine;

public class MinoDashAttackState : IState
{
    private MinoController controller;

    private Vector2 startPosition;

    private float dashTimer;
    private float telegraphDuration = 2f;
    private float maxDashDistance = 10f;

    private float recoveryTimer;
    private float recoveryDuration = 0.7f;

    private bool isRecovery;

    private bool isDash;

    public MinoDashAttackState(MinoController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        dashTimer = 0f;
        recoveryTimer = 0f;

        isDash = false;
        isRecovery = false;

        Vector2 direction = (
            controller.Target.position
            - controller.transform.position
        ).normalized;

        controller.View.LookDirection(direction);
        controller.LockDirection();

        controller.DashAttack.SetDirection(direction);
    }

    public void Tick()
    {
        if (!isDash && !isRecovery)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= telegraphDuration)
            {
                isDash = true;
                startPosition = controller.Rb.position;

                controller.View.SetDashSprite();
            }
        }

        if (isRecovery)
        {
            recoveryTimer += Time.deltaTime;

            if (recoveryTimer >= recoveryDuration)
            {
                controller.StartAttackCooldown();
                controller.ChangeState(controller.ChaseState);
            }
        }
    }

    public void FixedTick()
    {
        if (!isDash)
            return;

        bool hitPlayer =
            controller.DashAttack.Dash();

        if (hitPlayer)
        {
            EndDash();
            return;
        }

        float dashDistance = Vector2.Distance(
            startPosition,
            controller.Rb.position);

        if (dashDistance >= maxDashDistance)
        {
            EndDash();
        }
    }

    public void Exit()
    {
        controller.UnlockDirection();
    }

    private void EndDash()
    {
        isDash = false;
        isRecovery = true;

        recoveryTimer = 0f;

        controller.View.SetNormalSprite();
    }
}