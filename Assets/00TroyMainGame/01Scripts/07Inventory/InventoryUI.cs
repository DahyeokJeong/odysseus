using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Inventory inventory;

    [Header("UI")]
    [SerializeField] private Transform content;

    [Header("Prefab")]
    [SerializeField] private ItemSlotUI itemSlotPrefab;

    private void OnEnable()
    {
        inventory.OnItemAdded += AddItem;
        inventory.OnItemRemoved += RemoveItem;
    }

    private void OnDisable()
    {
        inventory.OnItemAdded -= AddItem;
        inventory.OnItemRemoved -= RemoveItem;
    }

    private void AddItem(InventoryItem item)
    {
        ItemSlotUI slot = Instantiate(itemSlotPrefab, content);
        slot.SetItem(item);
    }

    private void RemoveItem(InventoryItem item)
    {

    }
}