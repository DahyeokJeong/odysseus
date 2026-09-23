using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> items = new List<InventoryItem>();

    public IReadOnlyList<InventoryItem> Items => items;

    public event Action<InventoryItem> OnItemAdded;
    public event Action<InventoryItem> OnItemRemoved;

    public InventoryItem AddItem(ItemData data)
    {
        InventoryItem newItem = new InventoryItem(data);

        items.Add(newItem);

        OnItemAdded?.Invoke(newItem);

        return newItem;
    }

    public void RemoveItem(InventoryItem item)
    {
        if (!items.Remove(item))
            return;

        OnItemRemoved?.Invoke(item);
    }
}