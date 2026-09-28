using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Equipment equipment;
    [SerializeField] private Inventory inventory;

    public void Equip(InventoryItem item, EquipmentSlotType slotType)
    {
        if (!CanEquip(item, slotType))
            return;

        InventoryItem prevItem = equipment.GetItem(slotType);

        switch (slotType)
        {
            case EquipmentSlotType.Weapon:
                equipment.SetWeapon(item);
                break;

            case EquipmentSlotType.Helmet:
                equipment.SetHelmet(item);
                break;

            case EquipmentSlotType.Cloak:
                equipment.SetCloak(item);
                break;

            case EquipmentSlotType.Armor:
                equipment.SetArmor(item);
                break;

            case EquipmentSlotType.Shoes:
                equipment.SetShoes(item);
                break;

            case EquipmentSlotType.Acce01:
                equipment.SetAcce01(item);
                break;

            case EquipmentSlotType.Acce02:
                equipment.SetAcce02(item);
                break;

            default:
                return;
        }

        inventory.RemoveItem(item);

        if (prevItem != null)
            inventory.AddItem(prevItem);
    }

    public void UnEquip(EquipmentSlotType slotType)
    {
        InventoryItem item = null;

        switch (slotType)
        {
            case EquipmentSlotType.Weapon:
                item = equipment.Weapon;
                equipment.SetWeapon(null);
                break;

            case EquipmentSlotType.Helmet:
                item = equipment.Helmet;
                equipment.SetHelmet(null);
                break;

            case EquipmentSlotType.Cloak:
                item = equipment.Cloak;
                equipment.SetCloak(null);
                break;

            case EquipmentSlotType.Armor:
                item = equipment.Armor;
                equipment.SetArmor(null);
                break;

            case EquipmentSlotType.Shoes:
                item = equipment.Shoes;
                equipment.SetShoes(null);
                break;

            case EquipmentSlotType.Acce01:
                item = equipment.Acce01;
                equipment.SetAcce01(null);
                break;

            case EquipmentSlotType.Acce02:
                item = equipment.Acce02;
                equipment.SetAcce02(null);
                break;
        }

        if (item == null)
            return;

        inventory.AddItem(item);
    }

    private bool CanEquip(InventoryItem item, EquipmentSlotType slotType)
    {
        ItemType itemType = item.Data.ItemType;

        switch (slotType)
        {
            case EquipmentSlotType.Weapon:
                return itemType == ItemType.Weapon;

            case EquipmentSlotType.Helmet:
                return itemType == ItemType.Helmet;

            case EquipmentSlotType.Cloak:
                return itemType == ItemType.Cloak;

            case EquipmentSlotType.Armor:
                return itemType == ItemType.Armor;

            case EquipmentSlotType.Shoes:
                return itemType == ItemType.Shoes;

            case EquipmentSlotType.Acce01:
            case EquipmentSlotType.Acce02:
                return itemType == ItemType.Accessory;
        }

        return false;
    }
}