using UnityEngine;

public class BossNormalAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BossModel bossModel;

    [Header("Normal Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask playerLayer;

    public void Attack()
    {
        Debug.Log("Boss Normal Attack");

        Collider2D hit = Physics2D.OverlapBox(
            attackPoint.position,
            attackSize,
            0f,
            playerLayer);

        if (hit == null)
            return;

        IDamageable damageable = hit.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        Debug.Log($"Normal Attack Hit : {hit.name}");

        Vector2 hitDirection = (
            hit.transform.position - transform.position
        ).normalized;

        damageable.TakeDamage(
            bossModel.Attack,
            hitDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(
            attackPoint.position,
            attackSize
        );
    }
}