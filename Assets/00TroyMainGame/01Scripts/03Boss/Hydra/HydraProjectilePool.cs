using UnityEngine;
using UnityEngine.Pool;

public class HydraProjectilePool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private HydraPoisonProjectile projectilePrefab;

    [Header("Pool")]
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;

    private ObjectPool<HydraPoisonProjectile> pool;

    private void Awake()
    {
        pool = new ObjectPool<HydraPoisonProjectile>(
            CreateProjectile,
            OnGetProjectile,
            OnReleaseProjectile,
            OnDestroyProjectile,
            true,
            defaultCapacity,
            maxSize
        );
    }

    private HydraPoisonProjectile CreateProjectile()
    {
        HydraPoisonProjectile projectile =
            Instantiate(projectilePrefab, transform);

        projectile.SetPool(pool);
        projectile.gameObject.SetActive(false);

        return projectile;
    }

    private void OnGetProjectile(HydraPoisonProjectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectile(HydraPoisonProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(HydraPoisonProjectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public HydraPoisonProjectile Get()
    {
        return pool.Get();
    }
}