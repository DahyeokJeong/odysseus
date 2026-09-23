using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private ItemUI itemPrefab;

    private InventoryItem item;
    private ItemUI itemUI;

    public InventoryItem Item => item;

    public void SetItem(InventoryItem item)
    {
        this.item = item;

        itemUI = Instantiate(itemPrefab, transform);
        itemUI.SetItem(item);
    }
}