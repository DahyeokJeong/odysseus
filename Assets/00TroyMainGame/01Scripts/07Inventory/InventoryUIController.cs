using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject minimapUI;

    private void OnEnable()
    {
        EventBus.OnToggleInventory += ToggleInventory;
    }

    private void OnDisable()
    {
        EventBus.OnToggleInventory -= ToggleInventory;
    }

    private void ToggleInventory()
    {
        bool isOpen = !uiPanel.activeSelf;

        uiPanel.SetActive(isOpen);
        minimapUI.SetActive(!isOpen);
    }
}