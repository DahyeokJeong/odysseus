using UnityEngine;

public class MinoDashAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private BossModel bossModel;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private LayerMask playerLayer;

    private Vector2 dashDirection;

    public void SetDirection(Vector2 direction)
    {
        dashDirection = direction.normalized;
    }

    public bool Dash()
    {
        float moveDistance =
            dashSpeed * Time.fixedDeltaTime;

        Vector2 checkPos =
            rb.position + col.offset;

        RaycastHit2D hit = Physics2D.CapsuleCast(
            checkPos,
            col.size,
            col.direction,
            0f,
            dashDirection,
            moveDistance,
            playerLayer);

        if (hit.collider != null)
        {
            IDamageable damageable =
                hit.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Vector2 hitDirection = dashDirection;

                damageable.TakeDamage(
                    bossModel.Attack,
                    hitDirection);
            }

            return true;
        }

        Vector2 nextPos =
            rb.position
            + dashDirection * moveDistance;

        rb.MovePosition(nextPos);

        return false;
    }
}