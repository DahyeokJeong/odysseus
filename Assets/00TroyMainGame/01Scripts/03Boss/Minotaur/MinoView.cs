using UnityEngine;

public class MinoView : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sprite")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite dashSprite;

    [Header("Attack Point")]
    [SerializeField] private Transform leftAttackPoint;
    [SerializeField] private Transform rightAttackPoint;
    [SerializeField] private Transform upAttackPoint;
    [SerializeField] private Transform downAttackPoint;

    private Transform currentAttackPoint;

    private bool isHorizontal;

    public Transform CurrentAttackPoint => currentAttackPoint;
    public bool IsHorizontal => isHorizontal;

    public void LookDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            isHorizontal = true;

            if (direction.x < 0f)
            {
                spriteRenderer.flipX = false;
                currentAttackPoint = leftAttackPoint;
            }
            else
            {
                spriteRenderer.flipX = true;
                currentAttackPoint = rightAttackPoint;
            }
        }
        else
        {
            isHorizontal = false;

            if (direction.y > 0f)
                currentAttackPoint = upAttackPoint;
            else
                currentAttackPoint = downAttackPoint;
        }
    }

    public void SetNormalSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }

    public void SetDashSprite()
    {
        spriteRenderer.sprite = dashSprite;
    }
}