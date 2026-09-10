using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerController controller;
    private PlayerInputHandler inputHandler;
    private PlayerMovement movement;
    private Camera mainCam;

    public PlayerMoveState(
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
        Debug.Log("Player - Enter Move State");
    }

    public void Tick()
    {
        if (inputHandler.MovePressed)
        {
            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint( inputHandler.MousePos );

            mouseWorldPos.z = 0f;

            movement.SetTarget(mouseWorldPos);
        }

        movement.Move();

        if (!movement.IsMoving)
            controller.ChangeState(controller.IdleState);
    }

    public void FixedTick()
    {
    }

    public void Exit()
    {
        Debug.Log("Player - Exit Move State");
    }
}