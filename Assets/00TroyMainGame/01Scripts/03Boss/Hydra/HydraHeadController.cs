using UnityEngine;

public class HydraHeadController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraHead head;
    [SerializeField] private HydraGimmick gimmick;

    [Header("Gimmick")]
    [SerializeField] private Transform gimmickPos;

    private IState currentState;

    private SmallHeadIdleState idleState;
    private SmallHeadDownState downState;
    private SmallHeadSeveredState severedState;

    public HydraHead Head => head;
    public HydraGimmick Gimmick => gimmick;
    public Transform GimmickPos => gimmickPos;

    public SmallHeadIdleState IdleState => idleState;
    public SmallHeadDownState DownState => downState;
    public SmallHeadSeveredState SeveredState => severedState;

    private void Awake()
    {
        idleState = new SmallHeadIdleState(this);
        downState = new SmallHeadDownState(this);
        severedState = new SmallHeadSeveredState(this);
    }

    private void OnEnable()
    {
        head.OnDown += HandleDown;
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

    private void OnDisable()
    {
        head.OnDown -= HandleDown;
    }

    private void HandleDown(HydraHead head)
    {
        ChangeState(downState);
    }

    public void SeverHead()
    {
        Debug.Log($"{gameObject.name} Head Severed");

        // 추후 추가
        // 1. 절단된 Sprite로 변경
        // 2. HitBox 비활성화
        // 3. HydraController에 절단 수 전달
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