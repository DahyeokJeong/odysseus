using UnityEngine;

public class HydraHeadController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraHead head;
    [SerializeField] private HydraGimmick gimmick;
    [SerializeField] private HydraHeadView view;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private HydraPoisonProjectileAttack poisonAttack;

    [Header("Gimmick")]
    [SerializeField] private Transform gimmickPos;

    [Header("Hydra")]
    [SerializeField] private HydraController hydraController;

    [Header("Head")]
    [SerializeField] private GameObject hitBox;

    [Header("Sprite")]
    [SerializeField] private Sprite severedSprite;

    private IState currentState;

    private SmallHeadIdleState idleState;
    private SmallHeadAttackState attackState;
    private SmallHeadDownState downState;
    private SmallHeadSeveredState severedState;


    public HydraHead Head => head;
    public HydraGimmick Gimmick => gimmick;
    public HydraHeadView View => view;
    public PlayerInputHandler InputHandler => inputHandler;
    public HydraController HydraController => hydraController;
    public Transform GimmickPos => gimmickPos;
    public HydraPoisonProjectileAttack PoisonAttack => poisonAttack;


    public SmallHeadIdleState IdleState => idleState;
    public SmallHeadAttackState AttackState => attackState;
    public SmallHeadDownState DownState => downState;
    public SmallHeadSeveredState SeveredState => severedState;

    private void Awake()
    {
        idleState = new SmallHeadIdleState(this);
        attackState = new SmallHeadAttackState(this);
        downState = new SmallHeadDownState(this);
        severedState = new SmallHeadSeveredState(this);
    }

    private void OnEnable()
    {
        head.OnDown += HandleDown;
    }

    private void OnDisable()
    {
        head.OnDown -= HandleDown;
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

    private void HandleDown(HydraHead head)
    {
        ChangeState(downState);
    }

    public void SeverHead()
    {
        view.SetSeveredSprite(severedSprite);

        if (hitBox != null)
            hitBox.SetActive(false);

        hydraController.OnSmallHeadSevered();
    }

    public void StartAttack()
    {
        if (head.IsDown)
            return;

        ChangeState(attackState);
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