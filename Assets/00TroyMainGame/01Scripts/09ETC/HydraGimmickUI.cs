using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HydraGimmickUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Image timerBarFill;
    [SerializeField] private Image[] keyImages;
    [SerializeField] private RectTransform keyGroup;

    [Header("Key Sprite")]
    [SerializeField] private Sprite Q;
    [SerializeField] private Sprite W;
    [SerializeField] private Sprite E;
    [SerializeField] private Sprite R;

    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 screenOffset = new Vector2(0f, 80f);

    [Header("Color")]
    [SerializeField]
    private Color correctColor =
        new Color(0.4f, 0.6f, 0.85f, 1f);

    [SerializeField]
    private Color wrongColor =
        new Color(0.9f, 0.3f, 0.3f, 1f);

    [Header("Wrong Animation")]
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeDistance = 6f;
    [SerializeField] private float shakeSpeed = 60f;

    private Vector2 keyGroupStartPos;
    private Coroutine wrongCoroutine;

    private void Awake()
    {
        keyGroupStartPos = keyGroup.anchoredPosition;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(target.position);

        transform.position =
            screenPos + (Vector3)screenOffset;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTimer(float ratio)
    {
        timerBarFill.fillAmount =
            Mathf.Clamp01(ratio);
    }

    public void SetKey(int index, KeyCode key)
    {
        if (index < 0 ||
            index >= keyImages.Length)
            return;

        switch (key)
        {
            case KeyCode.Q:
                keyImages[index].sprite = Q;
                break;

            case KeyCode.W:
                keyImages[index].sprite = W;
                break;

            case KeyCode.E:
                keyImages[index].sprite = E;
                break;

            case KeyCode.R:
                keyImages[index].sprite = R;
                break;
        }
    }

    public void SetCorrect(int index)
    {
        if (index < 0 ||
            index >= keyImages.Length)
            return;

        keyImages[index].color = correctColor;
    }

    public void ResetKeys()
    {
        foreach (Image keyImage in keyImages)
        {
            keyImage.color = Color.white;
        }
    }

    public void PlayWrongAnimation(
        Action onComplete)
    {
        if (wrongCoroutine != null)
            StopCoroutine(wrongCoroutine);

        wrongCoroutine = StartCoroutine(
            WrongAnimation(onComplete)
        );
    }

    private IEnumerator WrongAnimation(
        Action onComplete)
    {
        // 전체 키 빨간색
        foreach (Image keyImage in keyImages)
        {
            keyImage.color = wrongColor;
        }

        float timer = 0f;

        // 좌우 흔들림
        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            float offset =
                Mathf.Sin(timer * shakeSpeed)
                * shakeDistance;

            keyGroup.anchoredPosition =
                keyGroupStartPos
                + Vector2.right * offset;

            yield return null;
        }

        // 원래 위치 복구
        keyGroup.anchoredPosition =
            keyGroupStartPos;

        ResetKeys();

        wrongCoroutine = null;

        onComplete?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        SetTimer(1f);
        ResetKeys();

        keyGroup.anchoredPosition =
            keyGroupStartPos;
    }

    public void Hide()
    {
        if (wrongCoroutine != null)
        {
            StopCoroutine(wrongCoroutine);
            wrongCoroutine = null;
        }

        keyGroup.anchoredPosition =
            keyGroupStartPos;

        gameObject.SetActive(false);
    }
}