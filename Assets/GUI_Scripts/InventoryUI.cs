using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public List<Row> rows;
    public ScrollRect inventoryUI;

    public void Start()
    {

    }
    private void Update()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            //for (int j = 0; j < rows[i]; j++)
            {
                //iconsOfItems[i][j].GetComponent<Image>().enabled = false;
            }
        }
    }
}

[Serializable]
public struct Row
{
    public GameObject[] iconOfItem;
}