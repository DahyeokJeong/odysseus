using TMPro;
using UnityEngine;

public class HydraFinalTimerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMP_Text timerText;

    [Header("Setting")]
    [SerializeField] private float warningTime = 5f;

    private HydraFinalGimmick finalGimmick;

    public void Init(HydraFinalGimmick gimmick)
    {
        finalGimmick = gimmick;
        timerPanel.SetActive(false);
    }

    private void Update()
    {
        if (finalGimmick == null || !finalGimmick.IsActive)
            return;

        float remainingTime = finalGimmick.RemainingTime;

        timerText.text = Mathf.CeilToInt(remainingTime).ToString();

        timerText.color = remainingTime <= warningTime
            ? Color.red
            : Color.white;
    }

    public void Show()
    {
        timerPanel.SetActive(true);
    }

    public void Hide()
    {
        timerPanel.SetActive(false);
    }
}