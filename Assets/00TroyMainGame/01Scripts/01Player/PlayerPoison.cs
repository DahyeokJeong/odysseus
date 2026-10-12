using UnityEngine;

public class PlayerPoison : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerModel model;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private PlayerHit hit;

    [Header("Poison")]
    [SerializeField] private float poisonDuration = 3f;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private float tickDamage = 2f;
    [SerializeField] private Color poisonColor = Color.green;

    private float durationTimer;
    private float tickTimer;

    private Color originalColor;
    private bool isPoisoned;

    public bool IsPoisoned => isPoisoned;
    public Color PoisonColor => poisonColor;

    private void Awake()
    {
        originalColor = sr.color;
    }

    private void Update()
    {
        if (!isPoisoned)
            return;

        if (model.CurrentHP <= 0f)
        {
            EndPoison();
            return;
        }

        durationTimer -= Time.deltaTime;
        tickTimer += Time.deltaTime;

        if (tickTimer >= tickInterval)
        {
            tickTimer -= tickInterval;
            model.ReduceHP(tickDamage);
        }

        if (durationTimer <= 0f || model.CurrentHP <= 0f)
            EndPoison();
    }

    public void ApplyPoison()
    {
        if (model.CurrentHP <= 0f)
            return;

        durationTimer = poisonDuration;

        if (isPoisoned)
            return;

        isPoisoned = true;
        tickTimer = 0f;

        if (hit == null || hit.IsHitFinished)
            sr.color = poisonColor;
    }

    private void EndPoison()
    {
        isPoisoned = false;

        durationTimer = 0f;
        tickTimer = 0f;

        if (hit == null || hit.IsHitFinished)
            sr.color = originalColor;
    }

    private void OnDisable()
    {
        if (isPoisoned)
            EndPoison();
    }
}