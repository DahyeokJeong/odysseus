using UnityEngine;

public enum DashHitType
{
    None,
    Player,
    Wall
}

public class MinoDashAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private BossModel bossModel;

    [Header("Dash")]
    [SerializeField] private Transform dashPoint;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDamageMultiplier = 2f;
    [SerializeField] private float dashTelegraphWidth = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    private Vector2 dashDirection;
    private float phaseMultiplier = 1f;

    public void SetDirection(Vector2 direction)
    {
        dashDirection = direction.normalized;
    }

    public DashHitType Dash()
    {
        float moveDistance = dashSpeed * phaseMultiplier * Time.fixedDeltaTime;

        Vector2 checkPos =
            rb.position + col.offset;

        int collisionLayer =
            playerLayer | wallLayer;

        RaycastHit2D hit = Physics2D.CapsuleCast(
            checkPos,
            col.size,
            col.direction,
            0f,
            dashDirection,
            moveDistance,
            collisionLayer
        );

        if (hit.collider != null)
        {
            // Player
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                IDamageable damageable =
                    hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(bossModel.Attack * dashDamageMultiplier * phaseMultiplier, dashDirection);
                }

                return DashHitType.Player;
            }

            // Wall
            if (((1 << hit.collider.gameObject.layer) & wallLayer) != 0)
            {
                BreakableWall breakableWall =
                    hit.collider.GetComponentInParent<BreakableWall>();

                if (breakableWall != null)
                {
                    breakableWall.Break();
                }

                return DashHitType.Wall;
            }
        }

        Vector2 nextPos =
            rb.position
            + dashDirection * moveDistance;

        rb.MovePosition(nextPos);

        return DashHitType.None;
    }

    public void ShowTelegraph(
        BossTelegraph telegraph,
        float dashDistance)
    {
        telegraph.SetColor(
            new Color(0f, 0.5f, 1f, 0.3f)
        );

        float angle =
            Mathf.Atan2(dashDirection.y, dashDirection.x)
            * Mathf.Rad2Deg;

        Vector2 position =
            (Vector2)dashPoint.position
            + dashDirection * (dashDistance * 0.5f);

        Vector2 size = new Vector2(
            dashDistance,
            dashTelegraphWidth
        );

        telegraph.Show(
            position,
            size,
            angle
        );
    }

    public void HideTelegraph(BossTelegraph telegraph)
    {
        telegraph.Hide();
    }

    public void SetPhaseMultiplier(float multiplier)
    {
        phaseMultiplier = multiplier;
    }
}