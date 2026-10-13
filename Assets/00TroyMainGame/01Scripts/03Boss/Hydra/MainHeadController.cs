using UnityEngine;

public class MainHeadController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraHead head;
    [SerializeField] private HydraPoisonBreath breath;
    [SerializeField] private HydraPoisonBarrage barrage;

    [Header("Hydra")]
    [SerializeField] private HydraController hydraController;

    public HydraController HydraController => hydraController;

    private IState currentState;

    private MainHeadIdleState idleState;
    private MainHeadBreathState breathState;
    private MainHeadBarrageState barrageState;

    public HydraHead Head => head;
    public HydraPoisonBreath Breath => breath;
    public HydraPoisonBarrage Barrage => barrage;

    public MainHeadIdleState IdleState => idleState;
    public MainHeadBreathState BreathState => breathState;
    public MainHeadBarrageState BarrageState => barrageState;

    private void Awake()
    {
        idleState = new MainHeadIdleState(this);
        breathState = new MainHeadBreathState(this);
        barrageState = new MainHeadBarrageState(this);
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

    public void ChangeState(IState newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}