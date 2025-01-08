using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public ItemStats stats;

    public void Upgrade()
    {
        stats = new ItemStats
        {
            Name = stats.Name,
            Description = stats.Description,
            Type = stats.Type,
            UpgradeLevel = stats.UpgradeLevel + 1,
            DamageToEnemies = stats.DamageToEnemies * stats.UpgradeLevel,
            DamageToModels = stats.DamageToModels * stats.UpgradeLevel
        };
    }
}


[Serializable]
public struct ItemStats
{
    public string Name;
    public string Description;
    public int Type; // 0: Magic Weapon, 1: Weapon, 2: Pickaxe, 3: Axe
    public string Rarity;
    public int UpgradeLevel;
    public int DamageToEnemies;
    public int DamageToModels;
}
