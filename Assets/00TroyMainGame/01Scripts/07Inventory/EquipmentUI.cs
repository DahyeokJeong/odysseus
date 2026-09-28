using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Equipment equipment;

    [Header("Slot")]
    [SerializeField] private EquipmentSlotUI weaponSlot;
    [SerializeField] private EquipmentSlotUI helmetSlot;
    [SerializeField] private EquipmentSlotUI cloakSlot;
    [SerializeField] private EquipmentSlotUI armorSlot;
    [SerializeField] private EquipmentSlotUI shoesSlot;
    [SerializeField] private EquipmentSlotUI acce01Slot;
    [SerializeField] private EquipmentSlotUI acce02Slot;

    private void OnEnable()
    {
        equipment.OnEquipmentChanged += Refresh;
    }

    private void OnDisable()
    {
        equipment.OnEquipmentChanged -= Refresh;
    }

    public void Refresh()
    {
        weaponSlot.SetItem(equipment.Weapon);
        helmetSlot.SetItem(equipment.Helmet);
        cloakSlot.SetItem(equipment.Cloak);
        armorSlot.SetItem(equipment.Armor);
        shoesSlot.SetItem(equipment.Shoes);
        acce01Slot.SetItem(equipment.Acce01);
        acce02Slot.SetItem(equipment.Acce02);
    }
}