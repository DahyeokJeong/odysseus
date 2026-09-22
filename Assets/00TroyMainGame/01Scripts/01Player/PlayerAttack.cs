using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private PlayerModel playerModel;

    [Header("Attack")]
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private LayerMask enemyLayer;

    private float nextAttackTime;

    public bool IsAttackFinished { get; private set; }
    public Vector2 AttackDirection { get; private set; }
    public Vector2 FacingDirection { get; private set; } = Vector2.right;
    public bool CanAttack => Time.time >= nextAttackTime;

    public void StartAttack()
    {
        IsAttackFinished = false;

        nextAttackTime = Time.time + playerModel.AttackCooldown;
    }

    public void EndAttack()
    {
        IsAttackFinished = true;
    }

    public void Attack()
    {
        Vector2 attackOrigin = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position,
                                                       playerModel.AttackRange,
                                                       enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Vector2 targetPos = hit.ClosestPoint(attackOrigin);

            Vector2 targetDirection = targetPos - attackOrigin;

            if (targetDirection.sqrMagnitude > 0.0001f)
            {
                float angle = Vector2.Angle(
                    FacingDirection,
                    targetDirection);

                if (angle > attackAngle * 0.5f)
                    continue;
            }

            IDamageable target = hit.GetComponentInParent<IDamageable>();

            if (target == null)
                continue;
            
            Vector2 hitDirection = ((Vector2)hit.transform.position - attackOrigin).normalized;

            target.TakeDamage(playerModel.Attack, hitDirection);
        }
    }

    public void SetAttackDirection(Vector2 mouseWorldPos)
    {
        Vector2 direction = mouseWorldPos - (Vector2)transform.position;

        if (direction.sqrMagnitude <= 0.0001f)
            return;

        AttackDirection = direction.normalized;

        UpdateFacingDirection();

        //Debug.Log($"Attack Direction : {AttackDirection}");
    }

    private void UpdateFacingDirection()
    {
        if (Mathf.Abs(AttackDirection.x) >= Mathf.Abs(AttackDirection.y))
        {
            FacingDirection = AttackDirection.x < 0f
                ? Vector2.left
                : Vector2.right;
        }
        else
        {
            FacingDirection = AttackDirection.y < 0f
                ? Vector2.down
                : Vector2.up;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (playerModel == null)
            return;

        Gizmos.color = Color.red;

        Vector3 attackDirection = FacingDirection;
        Vector3 prevPos = Vector3.zero;

        int segments = 30;

        for (int i = 0; i <= segments; i++)
        { 

            float angle = -attackAngle * 0.5f
                        + attackAngle * i / segments;

            Vector3 direction =
                Quaternion.Euler(0f, 0f, angle) * attackDirection;

            Vector3 nextPos =
                transform.position + direction * playerModel.AttackRange;

            if (i == 0)
            {
                Gizmos.DrawLine(transform.position, nextPos);
            }
            else
            {
                Gizmos.DrawLine(prevPos, nextPos);
            }

            if (i == segments)
                Gizmos.DrawLine(transform.position, nextPos);

            prevPos = nextPos;
        }
    }
}