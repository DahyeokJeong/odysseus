using UnityEngine;

public class HydraPoisonPool : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float lifeTime = 5f;
    [SerializeField, Range(0f, 1f)]
    private float slowMultiplier = 0.6f;
    [SerializeField] private float poisonInterval = 0.5f;

    private float lifeTimer;
    private float poisonTimer;

    private bool isActive;
    private bool isPlayerInside;

    private PlayerPoison playerPoison;
    private PlayerMovement playerMovement;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        RemovePlayerEffects();

        lifeTimer = lifeTime;
        poisonTimer = 0f;

        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
            return;

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            Deactivate();
            return;
        }

        if (!isPlayerInside || playerPoison == null)
            return;

        poisonTimer += Time.deltaTime;

        if(poisonTimer >= poisonInterval)
        {
            poisonTimer = 0f;
            playerPoison.ApplyPoison();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive || isPlayerInside)
            return;

        PlayerPoison poison = other.GetComponentInParent<PlayerPoison>();

        PlayerMovement movement = other.GetComponentInParent<PlayerMovement>();

        if (poison == null || movement == null)
            return;

        playerPoison = poison;
        playerMovement = movement;

        isPlayerInside = true;
        poisonTimer = 0f;

        playerMovement.ApplySlow(slowMultiplier);
        playerPoison.ApplyPoison();

        Debug.Log("독 장판 진입 - 둔화 및 중독");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActive || !isPlayerInside)
            return;

        PlayerMovement movement =
            other.GetComponentInParent<PlayerMovement>();

        if (movement != playerMovement)
            return;

        RemovePlayerEffects();

        Debug.Log("독 장판 이탈 - 둔화 해제");
    }

    private void RemovePlayerEffects()
    {
        if (isPlayerInside && playerMovement != null)
            playerMovement.RemoveSlow();

        isPlayerInside = false;
        playerPoison = null;
        playerMovement = null;

        poisonTimer = 0f;
    }

    private void Deactivate()
    {
        isActive = false;
        isPlayerInside = false;
        playerPoison = null;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        RemovePlayerEffects();
        isActive = false;
    }
}