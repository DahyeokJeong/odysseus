using UnityEngine;

public class SmallHeadSeveredState : IState
{
    private HydraHeadController controller;

    public SmallHeadSeveredState(HydraHeadController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        Debug.Log($"{controller.gameObject.name} Severed State");

        controller.SeverHead();
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