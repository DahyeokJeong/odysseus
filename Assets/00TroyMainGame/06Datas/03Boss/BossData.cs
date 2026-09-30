using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Data/BossData")]
public class BossData : ScriptableObject
{
    [Header("Stat")]
    [SerializeField] private float maxHP;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attack;
    [SerializeField] private float defence;

    [Header("Range")]
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;

    public float MaxHP => maxHP;
    public float MoveSpeed => moveSpeed;
    public float Attack => attack;
    public float Defence => defence;
    public float DetectRange => detectRange;
    public float AttackRange => attackRange;
}