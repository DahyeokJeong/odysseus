using UnityEngine;

public static class DamageCalculator
{
    public static float Calculate(float damage, float defence)
    {
        float finalDamage = Mathf.Max(0f, damage - defence);

        return finalDamage;
    }
}
