
using System.Collections;
using UnityEngine;

public class HydraHeadView : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer sr;

    [Header("Idle Sprite")]
    [SerializeField] private Sprite idleSprite01;
    [SerializeField] private Sprite idleSprite02;

    [Header("Animation")]
    [SerializeField] private float frameDuration = 0.4f;

    [Header("Minimap")]
    [SerializeField] private GameObject minimapMarker;

    [Header("Hit Effect")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private float frameTimer;
    private bool isSecondFrame;
    private bool isPlaying;

    private Color originalColor;
    private Coroutine hitCoroutine;

    private void Awake()
    {
        if (sr != null)
            originalColor = sr.color;
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        frameTimer += Time.deltaTime;

        if (frameTimer >= frameDuration)
        {
            frameTimer = 0f;
            isSecondFrame = !isSecondFrame;

            sr.sprite = isSecondFrame ? idleSprite02 : idleSprite01;
        }
    }

    public void PlayIdle()
    {
        frameTimer = Random.Range(0f, frameDuration);
        isSecondFrame = false;
        isPlaying = true;

        sr.sprite = idleSprite01;
    }

    public void StopIdle()
    {
        isPlaying = false;
    }

    public void HideHead()
    {
        StopIdle();
        StopHitFlash();

        sr.enabled = false;

        if (minimapMarker != null)
            minimapMarker.SetActive(false);
    }

    public void ShowHead()
    {
        sr.enabled = true;
        PlayIdle();

        if (minimapMarker != null)
            minimapMarker.SetActive(true);
    }

    public void PlayHitFlash()
    {
        if (sr == null || !sr.enabled)
            return;

        StopHitFlash();
        hitCoroutine = StartCoroutine(HitFlashRoutine());
    }

    private IEnumerator HitFlashRoutine()
    {
        sr.color = hitColor;

        yield return new WaitForSeconds(flashDuration);

        sr.color = originalColor;
        hitCoroutine = null;
    }

    private void StopHitFlash()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }

        if (sr != null)
            sr.color = originalColor;
    }

    private void OnDisable()
    {
        StopHitFlash();
    }
}
