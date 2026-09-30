using UnityEngine;
using System.Collections;

public class BossHit : MonoBehaviour, IDamageable
{
    [Header("Component")]
    [SerializeField] private BossModel bossModel;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Hit Effect")]
    [SerializeField] private float flashDuration = 0.1f;

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        float finalDamage = DamageCalculator.Calculate(damage, bossModel.Defence);

        bossModel.ReduceHP(finalDamage);

        StartCoroutine(HitFlash());
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = Color.white;
    }
}