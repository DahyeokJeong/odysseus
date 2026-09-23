using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "TROY/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;

    public int ItemID => itemID;
    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;

    public ItemType ItemType => (ItemType)(itemID / 1000);
    public ItemGrade ItemGrade => (ItemGrade)((itemID / 100) % 10);
    public int ItemNumber => itemID % 100;
}

public enum ItemType
{
    Weapon = 1,
    Helmet = 2,
    Cloak = 3,
    Armor = 4,
    Shoes = 5,
    Accessory = 6,
    Consumable = 7,
    Etc = 8
}

public enum ItemGrade
{
    Normal = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4
}