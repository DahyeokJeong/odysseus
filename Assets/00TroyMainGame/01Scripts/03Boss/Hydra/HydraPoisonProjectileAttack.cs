using UnityEngine;

public class HydraPoisonProjectileAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraProjectilePool projectilePool;
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [Header("Attack")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackDelay = 0.8f;

    private float attackTimer;
    private bool isAttacking;

    public bool IsAttackFinished { get; private set; }

    public void StartAttack()
    {
        attackTimer = 0f;
        isAttacking = true;
        IsAttackFinished = false;
    }

    public void Tick()
    {
        if (!isAttacking)
            return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            Fire();
            isAttacking = false;
            IsAttackFinished = true;
        }
    }

    private void Fire()
    {
        if (player == null || firePoint == null || projectilePool == null)
            return;

        Vector2 direction =
            ((Vector2)player.position - (Vector2)firePoint.position).normalized;

        HydraPoisonProjectile projectile = projectilePool.Get();

        projectile.transform.position = firePoint.position;
        projectile.Initialize(direction, damage);
    }

    public void StopAttack()
    {
        isAttacking = false;
        IsAttackFinished = false;
    }
}