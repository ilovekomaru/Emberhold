using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> InventoryItems;
    public UnityEvent<GameObject> activeObjectChanged;
    public GameObject nullModel;
    
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

    public void SetActiveItem(InventoryItem inventoryItem, bool noneItemActive = false)
    {
        if (noneItemActive)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                InventoryItems[i] = new InventoryItem
                {
                    isActive = false,
                    isFavorite = InventoryItems[i].isFavorite,
                    count = InventoryItems[i].count,
                    item = InventoryItems[i].item
                };
            }
            activeObjectChanged.Invoke(nullModel);
        }
        else
        {
            foreach (var item in InventoryItems)
            {
                if (item.isActive)
                {
                    activeObjectChanged.Invoke(item.item.Model);
                    return;
                }
            }
        }
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
