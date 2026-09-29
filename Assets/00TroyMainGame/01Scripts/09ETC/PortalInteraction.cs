using UnityEngine;

public class PortalInteraction : MonoBehaviour, IInteractable
{
    [Header("Component")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Portal")]
    [SerializeField] private Transform destination;

    public string InteractionText => "Enter";

    public void Interact()
    {
        playerMovement.Teleport(destination.position);
    }
}