using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public int EnemyID;
    public string EnemyName;

    [Header("Stat")]
    public float MaxHP;
    public float Attack;
    public float Defense;
    public float MoveSpeed;

    [Header("Range")]
    public float DetectRange;
    public float AttackRange;

    [Header("Attack")]
    public float AttackSpeed;
}
