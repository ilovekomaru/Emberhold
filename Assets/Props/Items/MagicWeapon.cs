using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class MagicWeapon : MonoBehaviour
{
    public MagicWeaponStats stats;
}


[Serializable]
public struct MagicWeaponStats
{
    public int Type; // 0: Палочки и посохи, 1: Spell book, 2: Mana Vault
    public string Rarity;
    public List<Spell> ListOfSpells;
    public int VaultOfMana;
    public int VaultOfManaMaxSize;
}
