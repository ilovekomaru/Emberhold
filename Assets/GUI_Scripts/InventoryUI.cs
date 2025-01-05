using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public List<GameObject> iconsOfItem;
    public ScrollRect inventoryUI;
    public TMP_Text inventorySpace;

    public void Start()
    {

    }
    private void Update()
    {
        for (int i = 0; i < inventory.InventoryItems.Count; i++)
        {
            iconsOfItem[i].GetComponent<Image>().enabled = true;
        }
        for (int i = inventory.InventoryItems.Count; i < 50; i++)
        {
            iconsOfItem[i].GetComponent<Image>().enabled = false;
        }

        inventorySpace.text = $"{inventory.currentSize}/{inventory.maxSize}";
    }
}
