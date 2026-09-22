using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

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

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void ReduceHP(float damage)
    {
        currentHP = Mathf.Max(0f, currentHP - damage);
    }
}
