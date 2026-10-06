using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class HydraGimmick : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraGimmickUI gimmickUI;

    [Header("Gimmick")]
    [SerializeField] private int sequenceLength = 4;
    [SerializeField] private float timeLimit = 5f;

    public event Action OnSuccess;
    public event Action OnFail;

    private KeyCode[] keyPool =
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R
    };

    private KeyCode[] sequence;

    private float timer;
    private int currentIndex;

    private bool isRunning;
    private bool isAnimating;

    private void Update()
    {
        if (!isRunning)
            return;

        UpdateTimer();

        if (!isAnimating)
            CheckInput();
    }

    public void StartGimmick(Transform gimmickPos)
    {
        CreateSequence();

        timer = timeLimit;
        currentIndex = 0;

        isRunning = true;
        isAnimating = false;

        gimmickUI.SetTarget(gimmickPos);

        gimmickUI.Show();
        gimmickUI.ResetKeys();

        SetSequenceUI();
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        float ratio = Mathf.Clamp01(timer / timeLimit);

        gimmickUI.SetTimer(ratio);

        if (timer <= 0f)
        {
            FailGimmick();
        }
    }

    private void CheckInput()
    {
        KeyCode inputKey = GetInputKey();

        if (inputKey == KeyCode.None)
            return;

        // 정답
        if (inputKey == sequence[currentIndex])
        {
            gimmickUI.SetCorrect(currentIndex);

            currentIndex++;

            Debug.Log(
                $"Hydra Gimmick : {currentIndex} / {sequence.Length}"
            );

            if (currentIndex >= sequence.Length)
            {
                SuccessGimmick();
            }

            return;
        }

        // 오답
        isAnimating = true;

        gimmickUI.PlayWrongAnimation(ResetSequence);
    }

    private KeyCode GetInputKey()
    {
        if (Keyboard.current == null)
            return KeyCode.None;

        if (Keyboard.current.qKey.wasPressedThisFrame)
            return KeyCode.Q;

        if (Keyboard.current.wKey.wasPressedThisFrame)
            return KeyCode.W;

        if (Keyboard.current.eKey.wasPressedThisFrame)
            return KeyCode.E;

        if (Keyboard.current.rKey.wasPressedThisFrame)
            return KeyCode.R;

        return KeyCode.None;
    }

    private void CreateSequence()
    {
        sequence = new KeyCode[sequenceLength];

        for (int i = 0; i < sequence.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(
                0,
                keyPool.Length
            );

            sequence[i] = keyPool[randomIndex];
        }
    }

    private void SetSequenceUI()
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            gimmickUI.SetKey(
                i,
                sequence[i]
            );
        }
    }

    private void ResetSequence()
    {
        currentIndex = 0;

        CreateSequence();

        gimmickUI.ResetKeys();
        SetSequenceUI();

        isAnimating = false;

        Debug.Log("Wrong Key - New Sequence");
    }

    private void SuccessGimmick()
    {
        isRunning = false;
        isAnimating = false;

        Debug.Log("Hydra Gimmick Success");

        gimmickUI.Hide();

        OnSuccess?.Invoke();
    }

    private void FailGimmick()
    {
        isRunning = false;
        isAnimating = false;

        gimmickUI.SetTimer(0f);

        Debug.Log("Hydra Gimmick Failed");

        gimmickUI.Hide();

        OnFail?.Invoke();
    }
}