using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropArea : MonoBehaviour, IDropHandler
{
    [Header("Component")]
    [SerializeField] private EquipmentManager equipmentManager;

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI itemUI = eventData.pointerDrag?.GetComponent<ItemUI>();

        if (itemUI == null)
            return;

        if (itemUI.EquipmentSlot == null)
            return;

        equipmentManager.UnEquip(itemUI.EquipmentSlot.SlotType);
    }
}