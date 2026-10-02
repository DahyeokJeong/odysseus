using UnityEngine;
using System.Collections.Generic;

public class MinoSweepAttack : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BossModel bossModel;

    [Header("Sweep")]
    [SerializeField] private Vector2 horizontalAttackSize;
    [SerializeField] private Vector2 verticalAttackSize;
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    private Transform currentAttackPoint;
    private Vector2 currentAttackSize;

    public void Attack(
        Transform attackPoint,
        bool isHorizontal)
    {
        currentAttackPoint = attackPoint;
        currentAttackSize = GetAttackSize(isHorizontal);

        int attackLayer =
            playerLayer | wallLayer;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            attackPoint.position,
            currentAttackSize,
            0f,
            attackLayer
        );

        HashSet<BreakableWall> breakableWalls = new();

        foreach (Collider2D hit in hits)
        {
            int hitLayer = hit.gameObject.layer;

            if (((1 << hitLayer) & playerLayer) != 0)
            {
                IDamageable damageable =
                    hit.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector2 hitDirection = (
                        hit.transform.position
                        - transform.position
                    ).normalized;

                    damageable.TakeDamage(
                        bossModel.Attack * damageMultiplier,
                        hitDirection
                    );
                }

                continue;
            }

            if (((1 << hitLayer) & wallLayer) != 0)
            {
                BreakableWall breakableWall =
                    hit.GetComponentInParent<BreakableWall>();

                if (breakableWall != null)
                {
                    breakableWalls.Add(breakableWall);
                }
            }
        }

        foreach (BreakableWall wall in breakableWalls)
        {
            wall.Break();
        }
    }

    public void ShowTelegraph(
        BossTelegraph telegraph,
        Transform attackPoint,
        bool isHorizontal)
    {
        currentAttackPoint = attackPoint;
        currentAttackSize = GetAttackSize(isHorizontal);

        telegraph.SetColor(
            new Color(1f, 1f, 0f, 0.3f)
        );

        telegraph.Show(
            attackPoint.position,
            currentAttackSize
        );
    }

    public void HideTelegraph(BossTelegraph telegraph)
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
            currentAttackSize
        );
    }
}