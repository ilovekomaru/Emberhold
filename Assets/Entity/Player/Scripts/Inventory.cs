using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemName> itemNames;
    
    void Start()
    {
        
    }

    
    public GameObject GetItem()
    {
        return null;
    }

    void Update()
    {
        
    }
}

[Serializable]
public struct ItemName
{
    public string Name;
    public string Description;
    public GameObject Model;
}
