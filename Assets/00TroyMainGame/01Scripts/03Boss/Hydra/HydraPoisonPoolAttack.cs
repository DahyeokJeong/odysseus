using UnityEngine;

public class HydraPoisonPoolAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private HydraPoisonOrb poisonOrbPrefab;

    [Header("Attack")]
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

        if (attackTimer < attackDelay)
            return;

        Fire();

        isAttacking = false;
        IsAttackFinished = true;
    }

    private void Fire()
    {
        if (player == null || firePoint == null || poisonOrbPrefab == null)
            return;

        Vector2 targetPosition = player.position;

        HydraPoisonOrb orb = Instantiate(
            poisonOrbPrefab,
            firePoint.position,
            Quaternion.identity
        );

        orb.Initialize(targetPosition);
    }

    public void StopAttack()
    {
        isAttacking = false;
    }
}