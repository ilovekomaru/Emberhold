using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Item
{
    public string Name;
    public string Description;
    public string Type; // Resource, Weapon, Tool
    public int DamageToEnemies;
    public int DamageToModels;
    public GameObject Model;
}
