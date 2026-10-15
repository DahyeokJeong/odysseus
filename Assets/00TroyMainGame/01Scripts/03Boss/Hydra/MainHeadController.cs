using UnityEngine;

public class MainHeadController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraHead head;
    [SerializeField] private HydraPoisonBreath breath;
    [SerializeField] private HydraPoisonBarrage barrage;
    [SerializeField] private HydraPoisonPoolAttack poisonPoolAttack;

    [Header("Hydra")]
    [SerializeField] private HydraController hydraController;

    public HydraController HydraController => hydraController;

    private IState currentState;

    private MainHeadIdleState idleState;
    private MainHeadBreathState breathState;
    private MainHeadBarrageState barrageState;
    private MainHeadPoisonPoolState poisonPoolState;

    public HydraHead Head => head;
    public HydraPoisonBreath Breath => breath;
    public HydraPoisonBarrage Barrage => barrage;
    public HydraPoisonPoolAttack PoisonPoolAttack => poisonPoolAttack;

    public MainHeadIdleState IdleState => idleState;
    public MainHeadBreathState BreathState => breathState;
    public MainHeadBarrageState BarrageState => barrageState;
    public MainHeadPoisonPoolState PoisonPoolState => poisonPoolState;

    private void Awake()
    {
        idleState = new MainHeadIdleState(this);
        breathState = new MainHeadBreathState(this);
        barrageState = new MainHeadBarrageState(this);
        poisonPoolState = new MainHeadPoisonPoolState(this);
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    public void StartBreath()
    {
        ChangeState(breathState);
    }

    public void StartBarrage()
    {
        ChangeState(barrageState);
    }

    public void StartPoisonPool()
    {
        ChangeState(poisonPoolState);
    }

    public void StopAttack()
    {
        ChangeState(idleState);
    }

    public void ChangeState(IState newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}