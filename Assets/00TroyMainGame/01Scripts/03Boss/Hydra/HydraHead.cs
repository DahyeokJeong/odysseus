using UnityEngine;
using System;

public class HydraHead : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;

    [Header("Head")]
    [SerializeField] private bool isMainHead;

    private float currentHP;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsMainHead => isMainHead;
    public bool IsDown => currentHP <= 0f;

    public event Action<HydraHead> OnDown;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        if (IsDown)
            return;

        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);

        Debug.Log($"{gameObject.name} HP : {currentHP}");

        if (currentHP <= 0f)
            OnDown?.Invoke(this);
    }

    public void Regenerate()
    {
        currentHP = maxHP;

        Debug.Log($"{gameObject.name} Regenerate : {currentHP}");
    }
}