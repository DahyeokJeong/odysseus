using UnityEngine;

public class MinoController : BossController
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void SelectPattern()
    {
        int pattern = attackSelectState.SelectAttack();

        switch (pattern)
        {
            case 0:
                ChangeState(normalAttackState);
                break;

            case 1:
                //ChargeState
                break;

            case 2:
                //RoarState
                break;
        }   
    }
}
