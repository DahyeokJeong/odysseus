using UnityEngine;

public class HydraController : MonoBehaviour
{
    [Header("Main Head")]
    [SerializeField] private HydraHead mainHead;
    [SerializeField] private MainHeadController mainHeadController;
    [SerializeField] private float breathInterval = 15f;

    [Header("Small Head")]
    [SerializeField] private HydraHeadController[] smallHeads;
    [SerializeField] private float attackInterval = 2f;

    private float attackTimer;
    private float breathTimer;

    private bool isSmallHeadAttacking;
    private bool isMainHeadBreathing;
    private bool isBreathPending;

    private int currentHeadIndex;
    private int severedHeadCount;

    public HydraHead MainHead => mainHead;
    public HydraHeadController[] SmallHeads => smallHeads;
    public int SeveredHeadCount => severedHeadCount;
    public bool IsMainPhase => severedHeadCount >= smallHeads.Length;

    private void Start()
    {
        mainHead.SetInvincible(true);
    }

    private void Update()
    {
        UpdateBreathTimer();

        if (isMainHeadBreathing)
            return;

        if (isBreathPending)
        {
            if (!isSmallHeadAttacking)
                StartMainHeadBreath();

            return;
        }

        if (IsMainPhase || isSmallHeadAttacking)
            return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
            StartNextSmallHeadAttack();
    }

    private void UpdateBreathTimer()
    {
        if (isMainHeadBreathing || isBreathPending)
            return;

        breathTimer += Time.deltaTime;

        if (breathTimer >= breathInterval)
            isBreathPending = true;
    }

    private void StartMainHeadBreath()
    {
        if (mainHeadController == null)
            return;

        isBreathPending = false;
        isMainHeadBreathing = true;

        attackTimer = 0f;

        mainHeadController.StartBreath();
    }

    public void EndMainHeadBreath()
    {
        isMainHeadBreathing = false;
        breathTimer = 0f;
        attackTimer = 0f;
    }

    private void StartNextSmallHeadAttack()
    {
        if (smallHeads.Length == 0)
            return;

        for (int i = 0; i < smallHeads.Length; i++)
        {
            HydraHeadController head = smallHeads[currentHeadIndex];

            currentHeadIndex =
                (currentHeadIndex + 1) % smallHeads.Length;

            if (head == null || head.Head.IsDown)
                continue;

            isSmallHeadAttacking = true;
            head.StartAttack();
            return;
        }

        attackTimer = 0f;
    }

    public void EndSmallHeadAttack()
    {
        isSmallHeadAttacking = false;
        attackTimer = 0f;
    }

    public void OnSmallHeadSevered()
    {
        severedHeadCount++;

        if (IsMainPhase)
            StartMainPhase();
    }

    private void StartMainPhase()
    {
        mainHead.SetInvincible(false);
    }
}