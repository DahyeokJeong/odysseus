using UnityEngine;

public class HydraPoisonPool : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float lifeTime = 5f;

    private float lifeTimer;
    private bool isActive;
    private bool isPlayerInside;

    private PlayerPoison playerPoison;

    public void Initialize()
    {
        lifeTimer = lifeTime;
        isActive = true;
        isPlayerInside = false;
        playerPoison = null;
    }

    private void Update()
    {
        if (!isActive)
            return;

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        PlayerPoison poison =
            other.GetComponentInParent<PlayerPoison>();

        if (poison == null)
            return;

        playerPoison = poison;
        isPlayerInside = true;

        Debug.Log("독 장판 진입");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActive || playerPoison == null)
            return;

        PlayerPoison poison =
            other.GetComponentInParent<PlayerPoison>();

        if (poison != playerPoison)
            return;

        isPlayerInside = false;
        playerPoison = null;

        Debug.Log("독 장판 이탈");
    }

    private void Deactivate()
    {
        isActive = false;
        isPlayerInside = false;
        playerPoison = null;

        gameObject.SetActive(false);
    }
}
