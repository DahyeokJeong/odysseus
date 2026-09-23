using UnityEngine;

public class InventoryItem
{
    public ItemData Data { get; private set; }
    public int Enforce {  get; private set; }

    public InventoryItem(ItemData data)
    {
        Data = data;
        Enforce = 0;
    }

    public void SetEnforce(int enforce)
    {
        Enforce = enforce;
    }
}
