using UnityEngine;
using System;
using System.Collections;

public class EnemyHit : MonoBehaviour, IDamageable
{
    [Header("Component")]
    [SerializeField] private EnemyModel model;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;

    [Header("Hit")]
    [SerializeField] private float hitDuration = 0.2f;
    [SerializeField] private float knockbackDistance = 0.2f;

    public bool IsHitFinished { get; private set; }

    public event Action OnHit;

    private Color originalColor;
    private Coroutine hitCoroutine;
    private Vector2 hitDirection;

    private void Awake()
    {
        originalColor = sr.color;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        if (model.CurrentHP <= 0f)
            return;

        float finalDamage = DamageCalculator.Calculate(damage, model.Defence);

        if (finalDamage <= 0f)
            return;

        model.ReduceHP(finalDamage);

        this.hitDirection = hitDirection.normalized;

        OnHit?.Invoke();
    }

    public void StartHit()
    {
        if (hitCoroutine != null)
            StopCoroutine(hitCoroutine);

        hitCoroutine = StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        IsHitFinished = false;

        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + hitDirection * knockbackDistance;

        sr.color = Color.red;

        float elapsed = 0f;

        while (elapsed < hitDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = Mathf.Clamp01(elapsed / hitDuration);

            rb.MovePosition(Vector2.Lerp(startPos, targetPos, t));

            yield return new WaitForFixedUpdate();
        }

        sr.color = originalColor;

        IsHitFinished = true;
        hitCoroutine = null;
    }
}