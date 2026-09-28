using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> items = new List<InventoryItem>();

    public IReadOnlyList<InventoryItem> Items => items;

    public event Action<InventoryItem> OnItemAdded;
    public event Action<InventoryItem> OnItemRemoved;

    // 새로운 Item 생성 (처음 아이템 획득)
    public InventoryItem AddItem(ItemData data)
    {
        InventoryItem newItem = new InventoryItem(data);

        items.Add(newItem);

        OnItemAdded?.Invoke(newItem);

        return newItem;
    }

    // 기존 객체 그대로 다시 추가 (장비 해제 시)
    public void AddItem(InventoryItem item)
    {
        if (item == null)
            return;

        items.Add(item);

        OnItemAdded?.Invoke(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (!items.Remove(item))
            return;

        OnItemRemoved?.Invoke(item);
    }
}