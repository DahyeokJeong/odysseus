using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private BossModel bossModel;

    [Header("Collision")]
    [SerializeField] private LayerMask collisionLayer;

    public void Move(Vector2 moveDirection)
    {
        moveDirection = moveDirection.normalized;

        float moveDistance =
            bossModel.MoveSpeed * Time.fixedDeltaTime;

        // 1. 목표 방향
        if (TryMove(
            moveDirection,
            moveDistance))
            return;

        Vector2 horizontalDirection =
            new Vector2(
                Mathf.Sign(moveDirection.x),
                0f
            );

        Vector2 verticalDirection =
            new Vector2(
                0f,
                Mathf.Sign(moveDirection.y)
            );

        // 2. 목표 방향에서 비중이 큰 축부터 확인
        if (Mathf.Abs(moveDirection.x) >
            Mathf.Abs(moveDirection.y))
        {
            if (TryMove(
                horizontalDirection,
                moveDistance))
                return;

            if (TryMove(
                verticalDirection,
                moveDistance))
                return;

            // 3. 막혀 있으면 반대 Y 방향으로 회피
            TryMove(
                -verticalDirection,
                moveDistance);
        }
        else
        {
            if (TryMove(
                verticalDirection,
                moveDistance))
                return;

            if (TryMove(
                horizontalDirection,
                moveDistance))
                return;

            // 3. 막혀 있으면 반대 X 방향으로 회피
            TryMove(
                -horizontalDirection,
                moveDistance);
        }
    }

    private bool TryMove(
        Vector2 moveDirection,
        float moveDistance)
    {
        if (moveDirection == Vector2.zero)
            return false;

        if (CheckWall(
            moveDirection,
            moveDistance))
            return false;

        MovePosition(
            moveDirection,
            moveDistance);

        return true;
    }

    private void MovePosition(
        Vector2 moveDirection,
        float moveDistance)
    {
        Vector2 nextPos =
            rb.position
            + moveDirection * moveDistance;

        rb.MovePosition(nextPos);
    }

    private bool CheckWall(
        Vector2 moveDirection,
        float moveDistance)
    {
        Vector2 checkPos =
            rb.position + col.offset;

        RaycastHit2D hit = Physics2D.CapsuleCast(
            checkPos,
            col.size,
            col.direction,
            0f,
            moveDirection,
            moveDistance,
            collisionLayer
        );

        if (hit.collider == null ||
            hit.collider.isTrigger)
            return false;

        return true;
    }
}