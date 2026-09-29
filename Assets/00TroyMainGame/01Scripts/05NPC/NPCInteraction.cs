using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string InteractionText => "Talk";

    public void Interact()
    {
        Debug.Log("NPC와 대화를 시작합니다.");
    }
}