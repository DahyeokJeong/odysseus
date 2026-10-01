using UnityEngine;

public class BossNormalAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BossModel bossModel;
    [SerializeField] private BossTelegraph telegraph;

    [Header("Normal Attack")]
    [SerializeField] private Vector2 horizontalAttackSize;
    [SerializeField] private Vector2 verticalAttackSize;
    [SerializeField] private LayerMask playerLayer;

    private Transform currentAttackPoint;
    private Vector2 currentAttackSize;

    public void Attack(Transform attackPoint, bool isHorizontal)
    {
        currentAttackPoint = attackPoint;
        currentAttackSize = GetAttackSize(isHorizontal);

        Collider2D hit = Physics2D.OverlapBox(
            attackPoint.position,
            currentAttackSize,
            0f,
            playerLayer);

        if (hit == null)
            return;

        IDamageable damageable = hit.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        Vector2 hitDirection = (
            hit.transform.position - transform.position
        ).normalized;

        damageable.TakeDamage(
            bossModel.Attack,
            hitDirection);
    }

    public void ShowTelegraph(Transform attackPoint, bool isHorizontal)
    {
        currentAttackPoint = attackPoint;
        currentAttackSize = GetAttackSize(isHorizontal);

        telegraph.Show(
            attackPoint.position,
            currentAttackSize);
    }

    public void HideTelegraph()
    {
        telegraph.Hide();
    }

    private Vector2 GetAttackSize(bool isHorizontal)
    {
        return isHorizontal
            ? horizontalAttackSize
            : verticalAttackSize;
    }

    private void OnDrawGizmosSelected()
    {
        if (currentAttackPoint == null)
            return;

        Gizmos.DrawWireCube(
            currentAttackPoint.position,
            currentAttackSize);
    }
}