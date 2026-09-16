using UnityEngine;

public class EnemyModel : MonoBehaviour, IDamageable
{
    [Header("Data")]
    [SerializeField] private EnemyData enemyData;

    public float CurrentHP { get; private set; }

    private void Awake()
    {
        CurrentHP = enemyData.MaxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP < 0f)
            CurrentHP = 0f;
    }
}
