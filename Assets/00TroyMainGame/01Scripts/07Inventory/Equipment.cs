using UnityEngine;
using System;

public class Equipment : MonoBehaviour
{
    private InventoryItem weapon;
    private InventoryItem helmet;
    private InventoryItem cloak;
    private InventoryItem armor;
    private InventoryItem shoes;
    private InventoryItem acce01;
    private InventoryItem acce02;

    public event Action OnEquipmentChanged;

    public InventoryItem Weapon => weapon;
    public InventoryItem Helmet => helmet;
    public InventoryItem Cloak => cloak;
    public InventoryItem Armor => armor;
    public InventoryItem Shoes => shoes;
    public InventoryItem Acce01 => acce01;
    public InventoryItem Acce02 => acce02;


    public void SetWeapon(InventoryItem item)
    {
        weapon = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetHelmet(InventoryItem item)
    {
        helmet = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetCloak(InventoryItem item)
    {
        cloak = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetArmor(InventoryItem item)
    {
        armor = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetShoes(InventoryItem item)
    {
        shoes = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetAcce01(InventoryItem item)
    {
        acce01 = item;
        OnEquipmentChanged?.Invoke();
    }

    public void SetAcce02(InventoryItem item)
    {
        acce02 = item;
        OnEquipmentChanged?.Invoke();
    }

    public InventoryItem GetItem(EquipmentSlotType slotType)
    {
        switch (slotType)
        {
            case EquipmentSlotType.Weapon:
                return weapon;

            case EquipmentSlotType.Helmet:
                return helmet;

            case EquipmentSlotType.Cloak:
                return cloak;

            case EquipmentSlotType.Armor:
                return armor;

            case EquipmentSlotType.Shoes:
                return shoes;

            case EquipmentSlotType.Acce01:
                return acce01;

            case EquipmentSlotType.Acce02:
                return acce02;
        }

        return null;
    }
}