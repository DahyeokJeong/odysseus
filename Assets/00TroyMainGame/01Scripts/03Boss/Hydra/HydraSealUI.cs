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
    [SerializeField, Range(0f, 360f)]
    private float successAngle = 45f;
    [SerializeField] private float targetAngle = 45f;

    [Header("Fail Effect")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeStrength = 10f;
    [SerializeField] private float shakeSpeed = 50f;

    [Header("Test")]
   // [SerializeField] private bool testMode = true;

    private float currentAngle;
    private float previousTimeScale;
    private bool isSealing;

    private Vector2 originalPosition;
    private float shakeTimer;

    public bool IsSealing => isSealing;

    public event Action<bool> OnSealFinished;

    private void Awake()
    {
        if (clockGroup != null)
            originalPosition = clockGroup.anchoredPosition;

        if (sealPanel != null)
            sealPanel.SetActive(false);
    }

    /*private void Start()
    {
        if (testMode)
            Open();
    }*/

    private void Update()
    {
        if (!isSealing)
            return;

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
            Mathf.DeltaAngle(currentAngle, targetAngle)
        );

        bool success = difference <= successAngle * 0.5f;

        if (!success)
        {
            Debug.Log("히드라 재생 봉인 실패!");
            shakeTimer = shakeDuration;
            return;
        }

        Debug.Log("히드라 재생 봉인 성공!");

        Close();
        OnSealFinished?.Invoke(true);
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
}
