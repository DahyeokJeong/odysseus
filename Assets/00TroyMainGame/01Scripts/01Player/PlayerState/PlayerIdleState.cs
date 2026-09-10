using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController controller;
    private PlayerInputHandler inputHandler;
    private PlayerMovement movement;
    private Camera mainCam;

    public PlayerIdleState(
        PlayerController controller,
        PlayerInputHandler inputHandler,
        PlayerMovement movement)
    {
        this.controller = controller;
        this.inputHandler = inputHandler;
        this.movement = movement;

        mainCam = Camera.main;
    }

    public void Enter()
    {
        Debug.Log("Player - Enter Idle State");
    }

    public void Tick()
    {
        if (!inputHandler.MovePressed)
            return;

        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint( inputHandler.MousePos );

        mouseWorldPosition.z = 0f;

        movement.SetTarget(mouseWorldPosition);

        controller.ChangeState(controller.MoveState);
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
        Debug.Log("Player - Exit Idle State");
    }
}
