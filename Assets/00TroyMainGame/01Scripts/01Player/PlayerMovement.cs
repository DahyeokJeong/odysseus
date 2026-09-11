using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 0.05f;

    [Header("Collision")]
    [SerializeField] private LayerMask wallLayer;

    private Vector2 targetPos;

    public bool IsMoving { get; private set; }

    public bool TrySetTarget(Vector2 target)
    {
        if (!TryGetNextPos(target, out _))
            return false;

        targetPos = target;
        IsMoving = true;

        return true;
    }

    public void Move()
    {
        if (!IsMoving)
            return;

        if (!TryGetNextPos(targetPos, out Vector2 nextPos))
        {
            IsMoving = false;
            return;
        }

        rb.MovePosition(nextPos);
    }

    private bool TryGetNextPos(Vector2 target, out Vector2 nextPos)
    {
        Vector2 currentPos = rb.position;

        Vector2 toTarget = target - currentPos;

        float distance = toTarget.magnitude;

        if (distance <= stopDistance)
        {
            nextPos = currentPos;
            return false;
        }

        Vector2 moveDirection = toTarget.normalized;

        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        float actualMoveDistance = Mathf.Min(moveDistance, distance);

        nextPos = currentPos + moveDirection * actualMoveDistance;

        if (CheckWall(nextPos))
            return false;

        return true;
    }

    private bool CheckWall(Vector2 nextPos)
    {
        Vector2 checkPos = nextPos + col.offset;
        
        Collider2D hit = Physics2D.OverlapCapsule(
            checkPos,
            col.size,
            col.direction,
            0f,
            wallLayer);

        if (hit != null)
        {
            return true;
        }

        return false;
    }
}