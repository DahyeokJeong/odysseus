using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    public void Move(Vector2 targetPos)
    {
        Vector2 nextPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed*Time.fixedDeltaTime);

        rb.MovePosition(nextPos);
    }
}
