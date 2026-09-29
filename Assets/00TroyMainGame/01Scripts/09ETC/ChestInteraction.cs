using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sprite")]
    [SerializeField] private Sprite openSprite;

    private bool isOpened;

    public string InteractionText => "Open";

    public void Interact()
    {
        if (isOpened)
            return;

        isOpened = true;
        spriteRenderer.sprite = openSprite;
    }
}