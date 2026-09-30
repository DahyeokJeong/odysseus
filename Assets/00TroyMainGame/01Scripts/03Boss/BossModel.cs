using UnityEngine;
using System;

public class BossModel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private BossData bossData;

    [Header("Current Stat")]
    [SerializeField] private float currentHP;

    public event Action OnHPChanged;
    public event Action OnDead;

    public float CurrentHP => currentHP;
    public float MaxHP => bossData.MaxHP;
    public float MoveSpeed => bossData.MoveSpeed;
    public float Attack => bossData.Attack;
    public float Defence => bossData.Defence;
    public float DetectRange => bossData.DetectRange;
    public float AttackRange => bossData.AttackRange;

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void ReduceHP(float damage)
    {
        currentHP = Mathf.Max(0f, currentHP - damage);

        OnHPChanged?.Invoke();

        if (currentHP <= 0f)
            OnDead?.Invoke();
    }
}