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
    public List<Spell> ListOfSpells; // nado kak-to pomeniat, poka ne pridumal
    public int Manapool;
    public int ManapoolUpgrade;
}
