using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public ItemStats stats;
}


[Serializable]
public struct ItemStats
{
    public string Name;
    public string Description;
    public int Type; // 0: Magic Weapon, 1: Weapon, 2: Pickaxe, 3: Axe
    public int DamageToEnemies;
    public int DamageToModels;
}
