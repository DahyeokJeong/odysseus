using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private int enemyID;
    [SerializeField] private string enemyName;

    [Header("Stat")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float attack = 10f;
    [SerializeField] private float defence = 0f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Range")]
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1.5f;

    public int EnemyID => enemyID;
    public string EnemyName => enemyName;

    public float MaxHP => maxHP;
    public float Attack => attack;
    public float Defence => defence;
    public float MoveSpeed => moveSpeed;

    public float DetectRange => detectRange;
    public float ChaseRange => chaseRange;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
}
