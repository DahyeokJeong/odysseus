using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInputHandler inputHandler;

    [Header("Interaction")]
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask interactionLayer;

    [Header("UI")]
    [SerializeField] private InteractionUI interactionUI;

    private IInteractable currentInteractable;

    private void Update()
    {
        FindInteractable();

        if (inputHandler.InteractPressed && currentInteractable != null)
            currentInteractable.Interact();
    }

    private void FindInteractable()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,
                                                 radius,
                                                 interactionLayer);

        if (hit == null)
        {
            currentInteractable = null;
            interactionUI.Hide();
            return;
        }

        currentInteractable = hit.GetComponent<IInteractable>();

        if (currentInteractable != null)
            interactionUI.Show(currentInteractable.InteractionText);
        else
            interactionUI.Hide();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
