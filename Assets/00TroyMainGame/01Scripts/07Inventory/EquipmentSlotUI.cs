using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : MonoBehaviour, IDropHandler
{
    [Header("Component")]
    [SerializeField] private EquipmentManager equipmentManager;

    [Header("Slot")]
    [SerializeField] private EquipmentSlotType slotType;

    [Header("Prefab")]
    [SerializeField] private ItemUI itemPrefab;

    private InventoryItem item;
    private ItemUI itemUI;

    public EquipmentSlotType SlotType => slotType;
    public InventoryItem Item => item;

    public void SetItem(InventoryItem item)
    {
        this.item = item;

        if (itemUI != null)
            Destroy(itemUI.gameObject);

        if (item == null)
            return;

        itemUI = Instantiate(itemPrefab, transform);

        RectTransform itemRect = itemUI.transform as RectTransform;
        itemRect.anchorMin = Vector2.zero;
        itemRect.anchorMax = Vector2.one;
        itemRect.offsetMin = Vector2.zero;
        itemRect.offsetMax = Vector2.zero;

        itemUI.SetItem(item);
        itemUI.SetEquipmentSlot(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI itemUI = eventData.pointerDrag?.GetComponent<ItemUI>();

        if (itemUI == null)
            return;

        equipmentManager.Equip(itemUI.Item, slotType);
    }
}