using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D col;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Collision")]
    [SerializeField] private LayerMask wallLayer;

    public void Move(Vector2 moveInput)
    {
        Vector2 currentPos = rb.position;

        Vector2 moveDirection = moveInput.normalized;

        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        Vector2 nextPos = currentPos + moveDirection * moveDistance;

        if (CheckWall(nextPos))
            return;

        rb.MovePosition(nextPos);
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
            return true;

        return false;
    }
}