using UnityEngine;

public class MinoStunState : IState
{
    private MinoController controller;

    private float stunTimer;
    private float stunDuration = 2f;

    public MinoStunState(MinoController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        Debug.Log("Stun start");
        controller.View.SetStunSprite();
        stunTimer = 0f;

        controller.LockDirection();
    }

    public void Tick()
    {
        stunTimer += Time.deltaTime;

        if (stunTimer >= stunDuration)
        {
            controller.StartAttackCooldown();
            controller.ChangeState(controller.ChaseState);
        }
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        controller.View.SetNormalSprite();
        Debug.Log("Stun end");
        controller.UnlockDirection();
    }
}