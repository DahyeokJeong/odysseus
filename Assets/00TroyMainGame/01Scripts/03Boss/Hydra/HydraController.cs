using UnityEngine;

public class HydraController : MonoBehaviour
{
    [Header("Head")]
    [SerializeField] private HydraHead mainHead;
    [SerializeField] private HydraHead[] smallHeads;

    public HydraHead MainHead => mainHead;
    public HydraHead[] SmallHeads => smallHeads;

    private void OnEnable()
    {
        foreach (HydraHead head in smallHeads)
        {
            head.OnDown += HandleHeadDown;
        }

        mainHead.OnDown += HandleHeadDown;
    }

    private void OnDisable()
    {
        foreach(HydraHead head in smallHeads)
        {
            head.OnDown -= HandleHeadDown;
        }

        mainHead.OnDown -= HandleHeadDown;
    }

    private void HandleHeadDown(HydraHead head)
    {
        Debug.Log("Small Head Down");
    }
}