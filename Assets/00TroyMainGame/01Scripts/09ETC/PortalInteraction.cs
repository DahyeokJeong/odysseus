using UnityEngine;

public class PortalInteraction : MonoBehaviour, IInteractable
{
    [Header("Component")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private MapManager mapManager;

    [Header("Portal")]
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject nextMapContents;

    public string InteractionText => "Enter";

    public void Interact()
    {
        mapManager.ChangeMap(nextMapContents);

        playerMovement.Teleport(destination.position);
    }
}