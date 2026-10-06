using UnityEngine;

public class SmallHeadDownState : IState
{
    private HydraHeadController controller;

    public SmallHeadDownState(HydraHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        Debug.Log($"{controller.gameObject.name} Down State");

        controller.Gimmick.OnSuccess += HandleSuccess;
        controller.Gimmick.OnFail += HandleFail;

        controller.Gimmick.StartGimmick(
            controller.GimmickPos
        );
    }

    public void Tick()
    {

    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        controller.Gimmick.OnSuccess -= HandleSuccess;
        controller.Gimmick.OnFail -= HandleFail;
    }

    private void HandleSuccess()
    {
        controller.ChangeState(
            controller.SeveredState
        );
    }

    private void HandleFail()
    {
        controller.Head.Regenerate();

        controller.ChangeState(
            controller.IdleState
        );
    }
}