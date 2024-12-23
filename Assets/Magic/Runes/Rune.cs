using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Rune
{
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; } // "Defense", "Attack", "Buff", "Debuff"
    public string Rarity { get; set; } // "Common", "Uncommon", "Rare", "Mythic" 
    public bool isSizing { get; set; } // Зависит ли эффект от объёма влитой маны?
}
