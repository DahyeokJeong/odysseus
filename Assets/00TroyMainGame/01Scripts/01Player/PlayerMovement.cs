using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 targetPos;

    public bool IsMoving { get; private set; }

    public void SetTarget(Vector2 target)
    {
        targetPos = target;

        IsMoving = true;
    }

    public void Move()
    {
        if (!IsMoving)
            return;

        Vector2 currentPos = transform.position;

        Vector2 toTarget = targetPos - currentPos;

        float distance = toTarget.magnitude;

        float moveDistance = moveSpeed * Time.deltaTime;

        if (distance <= moveDistance)
        {
            transform.position = new Vector3(
                                        targetPos.x,
                                        targetPos.y,
                                        transform.position.z);

            IsMoving = false;

            return;
        }

        Vector2 moveDirection = toTarget.normalized;

        Vector2 nextPos = currentPos + moveDirection * moveDistance;

        transform.position = new Vector3(
                                    nextPos.x,
                                    nextPos.y,
                                    transform.position.z);
    }
}