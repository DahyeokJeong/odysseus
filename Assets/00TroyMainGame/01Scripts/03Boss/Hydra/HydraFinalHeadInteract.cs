using UnityEngine;

public class HydraFinalHeadInteract : MonoBehaviour, IInteractable
{
    [Header("Hydra")]
    [SerializeField] private HydraFinalGimmick finalGimmick;

    public string InteractionText
    {
        get
        {
            if (finalGimmick == null || !finalGimmick.IsActive)
                return "";

            if (!finalGimmick.HasFire)
                return "";

            return "Fire";
        }
    }

    public void Interact()
    {
        if (finalGimmick == null || !finalGimmick.IsActive)
            return;

        finalGimmick.UseFire();
    }
}