using UnityEngine;

public class HydraController : MonoBehaviour
{
    public enum HydraPhase
    {
        SubPhase,
        MainPhase1,
        MainPhase2,
        MainPhase3
    }

    [Header("Phase")]
    [SerializeField] private HydraPhase currentPhase = HydraPhase.SubPhase;
    [SerializeField, Range(0f, 1f)] private float phase2HP = 0.7f;
    [SerializeField, Range(0f, 1f)] private float phase3HP = 0.4f;

    [Header("Main Head")]
    [SerializeField] private HydraHead mainHead;
    [SerializeField] private MainHeadController mainHeadController;
    [SerializeField] private float mainAttackInterval = 15f;

    [Header("Small Head")]
    [SerializeField] private HydraHeadController[] smallHeads;
    [SerializeField] private float attackInterval = 2f;

    [Header("Seal")]
    [SerializeField] private HydraSealUI sealUI;
    [SerializeField, Range(0f, 1f)] private float sealFailHealRatio = 0.2f;

    [Header("Final Gimmick")]
    [SerializeField] private HydraFinalGimmick finalGimmick;

    private bool isFinalGimmick;
    private bool isDead;

    public bool IsFinalGimmick => isFinalGimmick;
    public bool IsDead => isDead;

    private bool isSealPending;

    private float mainAttackTimer;
    private float attackTimer;

    private bool isMainHeadAttacking;
    private bool isMainAttackPending;
    private bool isSmallHeadAttacking;

    private int currentHeadIndex;
    private int severedHeadCount;

    public HydraPhase CurrentPhase => currentPhase;

    public HydraHead MainHead => mainHead;
    public HydraHeadController[] SmallHeads => smallHeads;
    public int SeveredHeadCount => severedHeadCount;
    public bool IsMainPhase => currentPhase != HydraPhase.SubPhase;

    private void OnEnable()
    {
        if (sealUI != null)
            sealUI.OnSealFinished += OnSealFinished;

        if (finalGimmick != null)
        {
            finalGimmick.OnGimmickSuccess += OnFinalGimmickSuccess;
            finalGimmick.OnGimmickFailed += OnFinalGimmickFailed;
        }
    }

    private void OnDisable()
    {
        if (sealUI != null)
            sealUI.OnSealFinished -= OnSealFinished;

        if (finalGimmick != null)
        {
            finalGimmick.OnGimmickSuccess -= OnFinalGimmickSuccess;
            finalGimmick.OnGimmickFailed -= OnFinalGimmickFailed;
        }
    }

    private void Start()
    {
        mainHead.SetInvincible(true);
    }

    private void Update()
    {
        if (isDead || isFinalGimmick)
            return;

        if (currentPhase == HydraPhase.MainPhase3 && mainHead.IsDown)
        {
            StartFinalGimmick();
            return;
        }

        UpdatePhase();

        if (isSealPending || mainHead.IsDown)
            return;

        UpdateMainAttackTimer();

        if (isMainHeadAttacking)
            return;

        if (isMainAttackPending)
        {
            if (!isSmallHeadAttacking)
                StartMainHeadAttack();

            return;
        }

        if (isSmallHeadAttacking)
            return;

        if (currentPhase != HydraPhase.SubPhase && severedHeadCount >= smallHeads.Length)
            return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
            StartNextSmallHeadAttack();
    }

    private void UpdatePhase()
    {
        if (currentPhase == HydraPhase.SubPhase || isSealPending || mainHead.IsDown)
            return;

        float hpRatio = mainHead.CurrentHP / mainHead.MaxHP;

        if (currentPhase == HydraPhase.MainPhase1 && hpRatio <= phase2HP)
        {
            StartSeal();
        }
        else if (currentPhase == HydraPhase.MainPhase2 && hpRatio <= phase3HP)
        {
            StartSeal();
        }
    }

    private void ChangePhase(HydraPhase newPhase)
    {
        if (currentPhase == newPhase)
            return;

        currentPhase = newPhase;

        Debug.Log($"[Hydra] Phase 변경 : {currentPhase}");
    }

    private void UpdateMainAttackTimer()
    {
        if (isMainHeadAttacking || isMainAttackPending)
            return;

        mainAttackTimer += Time.deltaTime;

        if (mainAttackTimer >= mainAttackInterval)
            isMainAttackPending = true;
    }

    private void StartMainHeadAttack()
    {
        if (mainHeadController == null)
            return;

        isMainAttackPending = false;
        isMainHeadAttacking = true;

        attackTimer = 0f;

        switch (currentPhase)
        {
            case HydraPhase.SubPhase:
            case HydraPhase.MainPhase1:
                mainHeadController.StartBreath();
                break;

            case HydraPhase.MainPhase2:
                if (Random.Range(0, 2) == 0)
                    mainHeadController.StartBreath();
                else
                    mainHeadController.StartBarrage();
                break;

            case HydraPhase.MainPhase3:
                int attackIndex = Random.Range(0, 3);

                switch (attackIndex)
                {
                    case 0:
                        mainHeadController.StartBreath();
                        break;

                    case 1:
                        mainHeadController.StartBarrage();
                        break;

                    case 2:
                        mainHeadController.StartPoisonPool();
                        break;
                }
                break;
        }
    }

    public void EndMainHeadAttack()
    {
        isMainHeadAttacking = false;
        mainAttackTimer = 0f;
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

        if (currentPhase == HydraPhase.SubPhase &&
            severedHeadCount >= smallHeads.Length)
        {
            StartMainPhase();
        }
    }

    private void StartMainPhase()
    {
        mainHead.SetInvincible(false);

        ChangePhase(HydraPhase.MainPhase1);
    }

    private void StartSeal()
    {
        if (sealUI == null)
            return;

        isSealPending = true;
        sealUI.Open();
    }

    private void OnSealFinished(bool success)
    {
        if (!isSealPending)
            return;

        isSealPending = false;

        if (!success)
        {
            RegenerateRandomHead();

            float threshold = currentPhase == HydraPhase.MainPhase1
                ? phase2HP
                : phase3HP;

            float targetHP = mainHead.MaxHP * threshold + 1f;

            float healAmount = Mathf.Max(
                mainHead.MaxHP * sealFailHealRatio,
                targetHP - mainHead.CurrentHP
            );

            mainHead.MainHeadHeal(healAmount);

            return;
        }

        switch (currentPhase)
        {
            case HydraPhase.MainPhase1:
                ChangePhase(HydraPhase.MainPhase2);
                break;

            case HydraPhase.MainPhase2:
                ChangePhase(HydraPhase.MainPhase3);
                break;
        }
    }

    private void RegenerateRandomHead()
    {
        int downCount = 0;

        foreach (HydraHeadController head in smallHeads)
        {
            if (head != null && head.Head.IsDown)
                downCount++;
        }

        if (downCount == 0)
            return;

        int randomIndex = Random.Range(0, downCount);

        foreach (HydraHeadController head in smallHeads)
        {
            if (head == null || !head.Head.IsDown)
                continue;

            if (randomIndex == 0)
            {
                head.RegenerateHead();

                severedHeadCount = Mathf.Max(0, severedHeadCount - 1);

                return;
            }

            randomIndex--;
        }
    }

    private void StartFinalGimmick()
    {
        if (isFinalGimmick || isDead)
            return;

        isFinalGimmick = true;

        isMainHeadAttacking = false;
        isMainAttackPending = false;
        isSmallHeadAttacking = false;

        mainAttackTimer = 0f;
        attackTimer = 0f;

        mainHeadController.StopAttack();

        Debug.Log("[Hydra] 최종 처치 기믹 시작!");

        if (finalGimmick != null)
            finalGimmick.StartGimmick();
    }

    private void OnFinalGimmickSuccess()
    {
        if (!isFinalGimmick || isDead)
            return;

        isFinalGimmick = false;
        isDead = true;

        mainHeadController.StopAttack();

        Debug.Log("[Hydra] 최종 기믹 성공! 히드라 사망");

        mainHead.gameObject.SetActive(false);
    }

    private void OnFinalGimmickFailed()
    {
        if (!isFinalGimmick || isDead)
            return;

        float reviveHP = mainHead.MaxHP * phase3HP * 0.5f;

        mainHead.ReviveMainHead(reviveHP);

        isFinalGimmick = false;

        mainAttackTimer = 0f;
        attackTimer = 0f;

        ChangePhase(HydraPhase.MainPhase3);

        Debug.Log($"[Hydra] 최종 기믹 실패! HP {reviveHP}로 부활, MainPhase3 전투 재개");
    }
}