using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private PlayerModel playerModel;

    [Header("Collision")]
    [SerializeField] private LayerMask collisionLayer;

    private float slowMultiplier = 1f;
    private int slowCount;

    public void Move(Vector2 moveInput)
    {
        Vector2 currentPos = rb.position;

        Vector2 moveDirection = moveInput.normalized;

        float moveDistance = playerModel.MoveSpeed * slowMultiplier * Time.fixedDeltaTime;

        Vector2 nextPos = currentPos + moveDirection * moveDistance;

        if (CheckWall(moveDirection, moveDistance))
            return;

        rb.MovePosition(nextPos);
    }

    public void ApplySlow(float multiplier)
    {
        slowCount++;
        slowMultiplier = Mathf.Min(slowMultiplier, multiplier);
    }

    public void RemoveSlow()
    {
        slowCount = Mathf.Max(0, slowCount - 1);

        if (slowCount == 0)
            slowMultiplier = 1f;
    }

    public void Teleport(Vector2 position)
    {
        rb.position = position;
    }

    private bool CheckWall(Vector2 moveDirection, float moveDistance)
    {
        Vector2 checkPos = rb.position + col.offset;

        RaycastHit2D hit = Physics2D.CapsuleCast(
            checkPos,
            col.size,
            col.direction,
            0f,
            moveDirection,
            moveDistance,
            collisionLayer
        );

        if (hit.collider == null || hit.collider.isTrigger)
            return false;

        // 벽에서 빠져나가는 방향의 이동은 허용
        if (Vector2.Dot(moveDirection, hit.normal) > 0f)
            return false;

        return true;
    }
}