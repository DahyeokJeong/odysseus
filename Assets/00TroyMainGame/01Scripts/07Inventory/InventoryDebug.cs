using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class InventoryDebug : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Inventory inventory;

    private List<ItemData> itemDatas = new List<ItemData>();

    private void Start()
    {
        LoadItemDatas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            AddRandomItem();
        }
    }

    private void LoadItemDatas()
    {
        Addressables.LoadAssetsAsync<ItemData>(
            "Item",
            itemData =>
            {
                itemDatas.Add(itemData);
            }
        );
    }

    private void AddRandomItem()
    {
        if (itemDatas.Count == 0)
            return;

        int randomIndex = Random.Range(0, itemDatas.Count);

        InventoryItem item = inventory.AddItem(itemDatas[randomIndex]);
    }
}