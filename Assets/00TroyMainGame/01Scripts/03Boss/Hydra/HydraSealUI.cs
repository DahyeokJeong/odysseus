using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HydraSealUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject sealPanel;
    [SerializeField] private RectTransform clockGroup;
    [SerializeField] private RectTransform needle;

    [Header("Setting")]
    [SerializeField] private float rotationSpeed = 240f;
    [SerializeField, Range(0f, 360f)] private float successAngle = 45f;
    [SerializeField] private float targetAngle = 45f;
    [SerializeField] private float timeLimit = 5f;

    [Header("Fail Effect")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeStrength = 10f;
    [SerializeField] private float shakeSpeed = 50f;

    private float currentAngle;
    private float remainingTime;
    private float previousTimeScale;
    private bool isSealing;

    private Vector2 originalPosition;
    private float shakeTimer;

    public bool IsSealing => isSealing;
    public float RemainingTime => remainingTime;

    public event Action<bool> OnSealFinished;

    private void Awake()
    {
        if (clockGroup != null)
            originalPosition = clockGroup.anchoredPosition;

        if (sealPanel != null)
            sealPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isSealing)
            return;

        remainingTime -= Time.unscaledDeltaTime;

        if(remainingTime <= 0f)
        {
            FinishSeal(false);
            return;
        }

        currentAngle += rotationSpeed * Time.unscaledDeltaTime;
        currentAngle = Mathf.Repeat(currentAngle, 360f);

        needle.localRotation =
            Quaternion.Euler(0f, 0f, -currentAngle);

        UpdateShake();

        if (Keyboard.current != null &&
            Keyboard.current.gKey.wasPressedThisFrame)
        {
            CheckSeal();
        }
    }

    public void Open()
    {
        if (isSealing || sealPanel == null ||
            needle == null || clockGroup == null)
            return;

        isSealing = true;
        currentAngle = 0f;
        remainingTime = timeLimit;
        shakeTimer = 0f;

        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        sealPanel.SetActive(true);

        needle.localRotation = Quaternion.identity;
        clockGroup.anchoredPosition = originalPosition;
    }

    private void CheckSeal()
    {
        float difference = Mathf.Abs(
            Mathf.DeltaAngle(currentAngle, targetAngle));

        bool success = difference <= successAngle * 0.5f;

        if (!success)
        {
            shakeTimer = shakeDuration;
            return;
        }

        FinishSeal(true);
    }

    private void FinishSeal(bool success)
    {
        Close();
        OnSealFinished?.Invoke(success);
    }

    private void UpdateShake()
    {
        if (shakeTimer <= 0f)
            return;

        shakeTimer -= Time.unscaledDeltaTime;

        if (shakeTimer <= 0f)
        {
            clockGroup.anchoredPosition = originalPosition;
            return;
        }

        float elapsed = shakeDuration - shakeTimer;
        float offset = Mathf.Sin(elapsed * shakeSpeed) * shakeStrength;

        clockGroup.anchoredPosition =
            originalPosition + new Vector2(offset, 0f);
    }

    private void Close()
    {
        isSealing = false;
        shakeTimer = 0f;

        if (clockGroup != null)
            clockGroup.anchoredPosition = originalPosition;

        if (sealPanel != null)
            sealPanel.SetActive(false);

        Time.timeScale = previousTimeScale;
    }

    private void OnDisable()
    {
        if (isSealing)
            Close();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (clockGroup == null)
            return;

        float radius = clockGroup.rect.width * 0.5f;

        Vector3 center = clockGroup.position;

        float startAngle = targetAngle - successAngle * 0.5f;
        float endAngle = targetAngle + successAngle * 0.5f;

        // 성공 범위 내부 표시
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);

        int segments = 20;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Lerp(startAngle, endAngle,
                (float)i / segments);

            float angle2 = Mathf.Lerp(startAngle, endAngle,
                (float)(i + 1) / segments);

            Vector3 point1 = GetSealGizmoPoint(center, radius, angle1);
            Vector3 point2 = GetSealGizmoPoint(center, radius, angle2);

            Gizmos.DrawLine(center, point1);
            Gizmos.DrawLine(point1, point2);
        }

        // 성공 범위 양쪽 경계선
        Gizmos.color = Color.green;

        Gizmos.DrawLine(center,
            GetSealGizmoPoint(center, radius, startAngle));

        Gizmos.DrawLine(center,
            GetSealGizmoPoint(center, radius, endAngle));

        // 목표 각도 중앙선
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(center,
            GetSealGizmoPoint(center, radius, targetAngle));
    }

    private Vector3 GetSealGizmoPoint(Vector3 center, float radius, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return center + new Vector3(
            Mathf.Sin(radian) * radius,
            Mathf.Cos(radian) * radius,
            0f
        );
    }
#endif
}
