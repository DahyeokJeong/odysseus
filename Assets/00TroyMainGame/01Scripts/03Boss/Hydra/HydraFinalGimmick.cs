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

    public bool IsActive => isActive;
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

        if (timerUI != null)
            timerUI.Show();

        Debug.Log("[Hydra] 최종 기믹 제한시간 시작!");
    }

    public void SuccessGimmick()
    {
        if (!isActive)
            return;

        isActive = false;

        if (timerUI != null)
            timerUI.Hide();

        OnGimmickSuccess?.Invoke();
    }

    private void FailGimmick()
    {
        isActive = false;

        if (timerUI != null)
            timerUI.Hide();

        Debug.Log("[Hydra] 최종 기믹 실패!");
        OnGimmickFailed?.Invoke();
    }
}