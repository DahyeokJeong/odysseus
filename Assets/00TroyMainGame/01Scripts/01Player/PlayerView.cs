using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform attackPoint;

    private Vector3 attackPointPos;

    private void Awake()
    {
        attackPointPos = attackPoint.localPosition;
    }

    public void UpdateFacing(Vector2 moveInput)
    {
        if (moveInput.sqrMagnitude < 0.001f)
            return;

        if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
        {
            spriteRenderer.flipX = false;
            return;
        }

        if (moveInput.x < 0f)
        {
            spriteRenderer.flipX = true;

            attackPoint.localPosition = new Vector3(-Mathf.Abs(attackPointPos.x),
                                                    attackPointPos.y,
                                                    attackPointPos.z);
        }

        else if (moveInput.x > 0f)
        {
            spriteRenderer.flipX = false;

            attackPoint.localPosition = new Vector3(Mathf.Abs(attackPointPos.x),
                                                    attackPointPos.y,
                                                    attackPointPos.z);  
        }
    }
}
