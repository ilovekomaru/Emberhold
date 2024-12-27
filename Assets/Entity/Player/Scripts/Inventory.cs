using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> InventoryItems;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AppendItemToInventory(Item item)
    {
        for (int i = 0; i < InventoryItems.Count; i++)
        {
            if (InventoryItems[i].item.Type == "Resource" && item.Name == InventoryItems[i].item.Name)
            {
                InventoryItem newItem = new InventoryItem
                {
                    isActive = false,
                    isFavorite = InventoryItems[i].isFavorite,
                    count = InventoryItems[i].count + 1,
                    item = item
                };
                InventoryItems[i] = newItem;

                return;
            }
        }

        var newItem2 = new InventoryItem
        {
            isFavorite = false,
            isActive = false,
            count = 1, 
            item = item
        };
        InventoryItems.Add(newItem2);
    }

    public GameObject GetActiveItem()
    {
        foreach (var item in InventoryItems)
        {
            if (item.isActive)
            {
                return item.item.Model;
            }
        }

        return null;
    }
}

[Serializable]
public struct InventoryItem
{
    public bool isFavorite;
    public bool isActive;
    public int count;
    public Item item;
}
