using System.Collections;
using UnityEngine;

public class MainHeadView : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer sr;

    [Header("Hit Effect")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private Color originalColor;
    private Coroutine hitCoroutine;

    private void Awake()
    {
        if (sr != null)
            originalColor = sr.color;
    }

    public void PlayHitFlash()
    {
        if (sr == null)
            return;

        if (hitCoroutine != null)
            StopCoroutine(hitCoroutine);

        hitCoroutine = StartCoroutine(HitFlashRoutine());
    }

    private IEnumerator HitFlashRoutine()
    {
        sr.color = hitColor;

        yield return new WaitForSeconds(flashDuration);

        sr.color = originalColor;
        hitCoroutine = null;
    }

    private void OnDisable()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }

        if (sr != null)
            sr.color = originalColor;
    }
}
