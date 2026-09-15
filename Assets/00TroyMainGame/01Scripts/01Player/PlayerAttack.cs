using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttackFinished { get; private set; }

    public void StartAttack()
    {
        IsAttackFinished = false;
    }

    public void EndAttack()
    {
        IsAttackFinished = true;
    }

    public void Attack()
    {
        Debug.Log("Player Attack");
    }
}