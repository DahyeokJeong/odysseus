using UnityEngine;
using UnityEngine.Pool;

public class HydraPoisonProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 5f;

    [Header("Collision")]
    [SerializeField] private LayerMask wallLayer;

    private IObjectPool<HydraPoisonProjectile> pool;

    private Vector2 direction;
    private float damage;
    private float lifeTimer;

    private bool isActive;

    public void SetPool(IObjectPool<HydraPoisonProjectile> pool)
    {
        this.pool = pool;
    }

    public void Initialize(Vector2 direction, float damage)
    {
        this.direction = direction.normalized;
        this.damage = damage;

        lifeTimer = lifeTime;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
            return;

        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
            Release();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        if (((1 << other.gameObject.layer) & wallLayer.value) != 0)
        {
            Release();
            return;
        }

        PlayerPoison poison =
            other.GetComponentInParent<PlayerPoison>();

        if (poison == null)
            return;

        IDamageable damageable =
            other.GetComponentInParent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(damage, direction);

        poison.ApplyPoison();

        Release();
    }

    private void Release()
    {
        if (!isActive)
            return;

        isActive = false;

        pool.Release(this);
    }
}