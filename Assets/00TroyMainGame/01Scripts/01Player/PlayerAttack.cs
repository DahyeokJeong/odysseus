using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Transform attackPoint;

    [Header("Attack")] // 추후 Stat과 연결
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 120f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Detection")]
    [SerializeField] private float detectionRadius = 0.15f;

    public bool IsAttackFinished { get; private set; }

    public void StartAttack()
    {
        IsAttackFinished = false;
    }

    public void EndAttack()
    {
        IsAttackFinished = true;
    }

    public void Attack()
    {
        if (attackPoint == null)
            return;

        Vector2 attackDirection = attackPoint.right;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position,
                                                       attackRange,
                                                       enemyLayer);

        foreach (Collider2D hit in hits)
        {
           
        }    
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        Vector3 attackDirection = attackPoint.right;
        Vector3 prevPos = Vector3.zero;

        int segments = 30;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -attackAngle * 0.5f
                        + attackAngle * i / segments;

            Vector3 direction =
                Quaternion.Euler(0f, 0f, angle) * attackDirection;

            Vector3 nextPos =
                attackPoint.position + direction * attackRange;

            if (i == 0)
            {
                Gizmos.DrawLine(attackPoint.position, nextPos);
            }
            else
            {
                Gizmos.DrawLine(prevPos, nextPos);
            }

            if (i == segments)
                Gizmos.DrawLine(attackPoint.position, nextPos);

            prevPos = nextPos;
        }
    }
}