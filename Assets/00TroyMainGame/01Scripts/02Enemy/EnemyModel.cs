using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EnemyData enemyData;

    public float CurrentHP { get; private set; }

    public float MaxHP => enemyData.MaxHP;
    public float Attack => enemyData.Attack;
    public float Defence => enemyData.Defence;
    public float MoveSpeed => enemyData.MoveSpeed;

    public float DetectRange => enemyData.DetectRange;
    public float ChaseRange => enemyData.ChaseRange;
    public float AttackRange => enemyData.AttackRange;
    public float AttackCooldown => enemyData.AttackCooldown;

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    public void ReduceHP(float damage)
    {
        CurrentHP = Mathf.Max(0f, CurrentHP - damage);

        Debug.Log($"{gameObject.name} HP : {CurrentHP}");
    }
}