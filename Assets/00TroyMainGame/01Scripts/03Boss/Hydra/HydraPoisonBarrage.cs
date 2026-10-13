
using UnityEngine;

public class HydraPoisonBarrage : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraProjectilePool projectilePool;
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [Header("Attack")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackDelay = 0.8f;
    [SerializeField] private int projectileCount = 8;
    [SerializeField] private float fireInterval = 0.15f;

    private float attackTimer;
    private float fireTimer;
    private int firedCount;

    private Vector2 attackDirection;
    private bool isAttacking;
    private bool isFiring;

    public bool IsAttackFinished { get; private set; }

    public void StartAttack()
    {
        if (player == null || firePoint == null || projectilePool == null)
        {
            IsAttackFinished = true;
            return;
        }

        attackDirection =
            ((Vector2)player.position -
             (Vector2)firePoint.position).normalized;

        attackTimer = 0f;
        fireTimer = 0f;
        firedCount = 0;

        isAttacking = true;
        isFiring = false;
        IsAttackFinished = false;
    }

    public void Tick()
    {
        if (!isAttacking)
            return;

        if (!isFiring)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer < attackDelay)
                return;

            isFiring = true;
            Fire();
            return;
        }

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        HydraPoisonProjectile projectile = projectilePool.Get();

        projectile.transform.position = firePoint.position;
        projectile.Initialize(attackDirection, damage);

        firedCount++;

        if (firedCount >= projectileCount)
        {
            isAttacking = false;
            isFiring = false;
            IsAttackFinished = true;
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        isFiring = false;
        IsAttackFinished = false;
    }
}
