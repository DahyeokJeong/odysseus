using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private EnemyModel model;
    [SerializeField] private Transform attackPoint;


    [Header("Attack")]
    [SerializeField] private Vector2 attackSize = new Vector2(2f, 1.2f);
    [SerializeField] private LayerMask playerLayer;

    private float nextAttackTime;

    public void TryAttack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + model.AttackCooldown;

        Attack();
    }

    private void Attack()
    {
        Collider2D hit = Physics2D.OverlapBox(attackPoint.position,
                                              attackSize,
                                              0f,
                                              playerLayer);

        if (hit == null)
            return;

        IDamageable target = hit.GetComponentInParent<IDamageable>();

        if (target == null)
            return;

        Vector2 hitDirection = (hit.transform.position - transform.position).normalized;

        target.TakeDamage(model.Attack, hitDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }
}
