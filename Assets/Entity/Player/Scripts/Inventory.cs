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
    public int maxSize = 50;
    public int currentSize = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        currentSize = InventoryItems.Count;
    }

    public void AppendItemToInventory(Item item)
    {
        if (currentSize < maxSize)
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
                    currentSize++;
                    
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
            currentSize++;
        }
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
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                if (InventoryItems[i].item.Name == inventoryItem.item.Name)
                {
                    InventoryItems[i] = new InventoryItem
                    {
                        isActive = true,
                        isFavorite = InventoryItems[i].isFavorite,
                        count = InventoryItems[i].count,
                        item = InventoryItems[i].item
                    };

                    activeObjectChanged.Invoke(InventoryItems[i].item.Model);
                }
                else
                {
                    InventoryItems[i] = new InventoryItem
                    {
                        isActive = false,
                        isFavorite = InventoryItems[i].isFavorite,
                        count = InventoryItems[i].count,
                        item = InventoryItems[i].item
                    };
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
