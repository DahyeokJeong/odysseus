using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemyModel model;

    public void Move(Vector2 targetPos)
    {
        Vector2 nextPos = Vector2.MoveTowards(rb.position, targetPos, model.MoveSpeed*Time.fixedDeltaTime);

        rb.MovePosition(nextPos);
    }
}