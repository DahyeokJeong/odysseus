using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Component")]
    [SerializeField] private Equipment equipment;

    [Header("Current Stat")]
    [SerializeField] private float currentHP;

    private float equipmentMaxHP;
    private float equipmentMoveSpeed;
    private float equipmentAttackRange;

    private float equipmentAttack;
    private float equipmentAttackCooldown;

    private float equipmentDefence;

    private float augmentMaxHP;
    private float augmentMoveSpeed;
    private float augmentAttackRange;

    private float augmentAttack;
    private float augmentAttackCooldown;

    private float augmentDefence;

    public event Action OnStatChanged;

    public float CurrentHP => currentHP;

    public float MaxHP => playerData.MaxHP + equipmentMaxHP + augmentMaxHP;

    public float Attack => playerData.Attack + equipmentAttack + augmentAttack;
    public float Defence => playerData.Defence + equipmentDefence + augmentDefence;
    public float MoveSpeed => playerData.MoveSpeed + equipmentMoveSpeed + augmentMoveSpeed;
    public float AttackRange => playerData.AttackRange + equipmentAttackRange + augmentAttackRange;
    public float AttackCooldown => Mathf.Max(
                                   0f,
                                   playerData.AttackCooldown
                                 + equipmentAttackCooldown 
                                 + augmentAttackCooldown);

    private void OnEnable()
    {
        equipment.OnEquipmentChanged += UpdateEquipmentStat;
    }

    private void OnDisable()
    {
        equipment.OnEquipmentChanged -= UpdateEquipmentStat;
    }

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void ReduceHP(float damage)
    {
        currentHP = Mathf.Max(0f, currentHP - damage);
    }

    public void SetEquipmentStat(float maxHP,
                                 float attack,
                                 float defence,
                                 float moveSpeed,
                                 float attackRange,
                                 float attackCooldown)
    {
        equipmentMaxHP = maxHP;
        equipmentAttack = attack;
        equipmentDefence = defence;
        equipmentMoveSpeed = moveSpeed;
        equipmentAttackRange = attackRange;
        equipmentAttackCooldown = attackCooldown;
    }

    private void UpdateEquipmentStat()
    {
        float maxHP = 0f;
        float attack = 0f;
        float defence = 0f;
        float moveSpeed = 0f;
        float attackRange = 0f;
        float attackCooldown = 0f;

        AddItemStat(equipment.Weapon);
        AddItemStat(equipment.Helmet);
        AddItemStat(equipment.Cloak);
        AddItemStat(equipment.Armor);
        AddItemStat(equipment.Shoes);
        AddItemStat(equipment.Acce01);
        AddItemStat(equipment.Acce02);

        SetEquipmentStat(
            maxHP,
            attack,
            defence,
            moveSpeed,
            attackRange,
            attackCooldown
        );
        
        OnStatChanged?.Invoke();

        void AddItemStat(InventoryItem item)
        {
            if (item == null)
                return;

            ItemData data = item.Data;

            maxHP += data.MaxHP;
            attack += data.Attack;
            defence += data.Defence;
            moveSpeed += data.MoveSpeed;
            attackRange += data.AttackRange;
            attackCooldown += data.AttackCooldown;
        }
    }
}
