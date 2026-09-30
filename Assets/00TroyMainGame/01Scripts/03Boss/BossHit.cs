using UnityEngine;

public class BossHit : MonoBehaviour, IDamageable
{
    [Header("Component")]
    [SerializeField] private BossModel bossModel;

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        float finalDamage = DamageCalculator.Calculate(damage, bossModel.Defence);

        bossModel.ReduceHP(finalDamage);
    }
}