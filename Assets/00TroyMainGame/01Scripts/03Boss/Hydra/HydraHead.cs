
using UnityEngine;
using System;

public class HydraHead : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;

    [Header("Head")]
    [SerializeField] private bool isMainHead;

    [Header("View")]
    [SerializeField] private HydraHeadView smallHeadView;
    [SerializeField] private MainHeadView mainHeadView;

    private float currentHP;
    private bool isInvincible;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsMainHead => isMainHead;
    public bool IsDown => currentHP <= 0f;
    public bool IsInvincible => isInvincible;

    public event Action<HydraHead> OnDown;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        if (IsDown || isInvincible || damage <= 0f)
            return;

        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);

        if (isMainHead)
            mainHeadView?.PlayHitFlash();
        else
            smallHeadView?.PlayHitFlash();

        Debug.Log($"{gameObject.name} HP : {currentHP}");

        if (currentHP <= 0f)
            OnDown?.Invoke(this);
    }

    public void Regenerate()
    {
        currentHP = maxHP;

        Debug.Log($"{gameObject.name} Regenerate : {currentHP}");
    }

    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    public void MainHeadHeal(float amount)
    {
        if (amount <= 0f || IsDown)
            return;

        float previousHP = currentHP;

        currentHP = Mathf.Min(currentHP + amount, maxHP);

        Debug.Log($"[Hydra] 메인 머리 HP 회복 : {previousHP} → {currentHP} (+{currentHP - previousHP})");
    }

    public void ReviveMainHead(float hp)
    {
        if (!isMainHead || hp <= 0f)
            return;

        currentHP = Mathf.Clamp(hp, 0f, maxHP);

        Debug.Log($"[Hydra] 메인 머리 부활 HP : {currentHP}");
    }
}
