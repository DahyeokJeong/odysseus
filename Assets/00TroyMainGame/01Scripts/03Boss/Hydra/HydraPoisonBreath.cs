
using UnityEngine;

public class HydraPoisonBreath : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private PlayerHit playerHit;
    [SerializeField] private PlayerPoison playerPoison;

    [Header("Attack")]
    [SerializeField] private float warningDuration = 2f;
    [SerializeField] private float breathDuration = 3f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float damageInterval = 1f;

    [Header("Range")]
    [SerializeField] private float attackRange = 15f;
    [SerializeField, Range(0f, 360f)] private float attackAngle = 150f;
    [SerializeField] private Vector2 attackDirection = Vector2.down;

    [Header("Collision")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Effect")]
    [SerializeField] private GameObject warningEffect;
    [SerializeField] private ParticleSystem breathEffect;

    private float stateTimer;
    private float damageTimer;

    private bool isWarning;
    private bool isBreathing;
    private bool hasHitPlayer;

    public bool IsBreathFinished { get; private set; }

    public void StartBreath()
    {
        stateTimer = 0f;
        damageTimer = 0f;

        isWarning = true;
        isBreathing = false;
        hasHitPlayer = false;

        IsBreathFinished = false;

        if (warningEffect != null)
            warningEffect.SetActive(true);

        if (breathEffect != null)
            breathEffect.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
    }

    public void Tick()
    {
        if (isWarning)
        {
            stateTimer += Time.deltaTime;

            if (stateTimer >= warningDuration)
                BeginBreath();

            return;
        }

        if (!isBreathing)
            return;

        stateTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;

        if (damageInterval > 0f)
        {
            while (damageTimer >= damageInterval)
            {
                damageTimer -= damageInterval;
                CheckDamage();
            }
        }

        if (stateTimer >= breathDuration)
        {
            isBreathing = false;
            IsBreathFinished = true;

            if (breathEffect != null)
                breathEffect.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmitting
                );
        }
    }

    private void BeginBreath()
    {
        isWarning = false;
        isBreathing = true;

        stateTimer = 0f;
        damageTimer = 0f;
        hasHitPlayer = false;

        if (warningEffect != null)
            warningEffect.SetActive(false);

        if (breathEffect != null)
            breathEffect.Play();
    }

    private void CheckDamage()
    {
        if (firePoint == null || player == null ||
            playerModel == null || playerPoison == null)
            return;

        if (playerModel.CurrentHP <= 0f)
            return;

        Vector2 origin = firePoint.position;
        Vector2 target = player.position;

        Vector2 toPlayer = target - origin;
        float distance = toPlayer.magnitude;

        if (distance > attackRange)
            return;

        Vector2 direction = attackDirection.normalized;

        if (direction == Vector2.zero)
            return;

        float angle = Vector2.Angle(direction, toPlayer);

        if (angle > attackAngle * 0.5f)
            return;

        RaycastHit2D wallHit = Physics2D.Raycast(
            origin,
            toPlayer.normalized,
            distance,
            wallLayer
        );

        if (wallHit.collider != null)
            return;

        if (!hasHitPlayer)
        {
            if (playerHit != null)
            {
                playerHit.TakeDamage(
                    damage,
                    toPlayer.normalized
                );
            }
            else
            {
                playerModel.ReduceHP(damage);
            }

            hasHitPlayer = true;
        }
        else
        {
            playerModel.ReduceHP(damage);
        }

        playerPoison.ApplyPoison();
    }

    public void StopBreath()
    {
        isWarning = false;
        isBreathing = false;
        hasHitPlayer = false;

        if (warningEffect != null)
            warningEffect.SetActive(false);

        if (breathEffect != null)
            breathEffect.Stop(
                true,
                ParticleSystemStopBehavior.StopEmitting
            );
    }
}
