using UnityEngine;

public class MainHeadController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraHead head;
    [SerializeField] private HydraPoisonBreath breath;

    [Header("Hydra")]
    [SerializeField] private HydraController hydraController;

    public HydraController HydraController => hydraController;

    private IState currentState;

    private MainHeadIdleState idleState;
    private MainHeadBreathState breathState;

    public HydraHead Head => head;
    public HydraPoisonBreath Breath => breath;

    public MainHeadIdleState IdleState => idleState;
    public MainHeadBreathState BreathState => breathState;

    private void Awake()
    {
        idleState = new MainHeadIdleState(this);
        breathState = new MainHeadBreathState(this);
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

    public void ChangeState(IState newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}