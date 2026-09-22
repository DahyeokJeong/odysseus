using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "TROY/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Attack")]
    [SerializeField] private float attack = 10f;
    [SerializeField] private float defence = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;

    public float MaxHP => maxHP;
    public float MoveSpeed => moveSpeed;
    public float Attack => attack;
    public float Defence => defence;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
}
