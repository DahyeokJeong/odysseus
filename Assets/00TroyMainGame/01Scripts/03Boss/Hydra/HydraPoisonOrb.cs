using UnityEngine;

public class HydraPoisonOrb : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private HydraPoisonPool poisonPoolPrefab;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalDistance = 0.1f;

    private Vector2 targetPosition;
    private bool isMoving;

    public void Initialize(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPosition) <= arrivalDistance)
            Explode();
    }

    private void Explode()
    {
        isMoving = false;

        HydraPoisonPool pool = Instantiate(
            poisonPoolPrefab,
            targetPosition,
            Quaternion.identity);

        pool.Initialize();

        Destroy(gameObject);
    }
}