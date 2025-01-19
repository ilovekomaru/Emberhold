using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public ItemStats stats;

    public void UpgradeAxe()
    {
        stats.AxeDamage += stats.AxeDamageUpgrade;
    }

    public void UpgradePickaxe()
    {
        stats.PickaxeDamage += stats.PickaxeDamageUpgrade;
    }
}


[Serializable]
public struct ItemStats
{
    public string Name;
    public string Description;
    public int Type;    // 0: Magic Weapon, 1: Magic Tome, 2: Pickaxe, 3: Axe
    public int Level;
    public int AxeDamage;
    public int AxeDamageUpgrade;
    public int PickaxeDamage;
    public int PickaxeDamageUpgrade;
}
