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

        RectTransform itemRect = itemUI.GetComponent<RectTransform>();

        itemRect.anchorMin = Vector2.zero;
        itemRect.anchorMax = Vector2.one;
        itemRect.offsetMin = Vector2.zero;
        itemRect.offsetMax = Vector2.zero;

        itemUI.SetItem(item);
    }
}