using System;
using UnityEngine;

public class HydraFinalGimmick : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float timeLimit = 10f;

    [Header("UI")]
    [SerializeField] private HydraFinalTimerUI timerUI;

    private float remainingTime;
    private bool isActive;
    private bool hasFire;

    public bool IsActive => isActive;
    public bool HasFire => hasFire;
    public float RemainingTime => remainingTime;

    public event Action OnGimmickSuccess;
    public event Action OnGimmickFailed;

    private void Start()
    {
        if (timerUI != null)
            timerUI.Init(this);
    }

    private void Update()
    {
        if (!isActive)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            FailGimmick();
        }
    }

    public void StartGimmick()
    {
        remainingTime = timeLimit;
        isActive = true;
        hasFire = false;

        if (timerUI != null)
            timerUI.Show();

        Debug.Log("[Hydra] ÃÖÁ¾ ±â¹Í Á¦ÇÑ½Ã°£ ½ÃÀÛ!");
    }

    public void TakeFire()
    {
        if (!isActive || hasFire)
            return;

        hasFire = true;

        Debug.Log("[Hydra] È­ÅêºÒ¿¡¼­ ºÒ È¹µæ!");
    }

    public void UseFire()
    {
        if (!isActive || !hasFire)
            return;

        SuccessGimmick();
    }

    public void SuccessGimmick()
    {
        if (!isActive)
            return;

        isActive = false;
        hasFire = false;

        if (timerUI != null)
            timerUI.Hide();

        OnGimmickSuccess?.Invoke();
    }

    private void FailGimmick()
    {
        isActive = false;
        hasFire = false;

        if (timerUI != null)
            timerUI.Hide();

        Debug.Log("[Hydra] ÃÖÁ¾ ±â¹Í ½ÇÆÐ!");

        OnGimmickFailed?.Invoke();
    }
}