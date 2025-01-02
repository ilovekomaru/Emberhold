using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rune
{
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; } // Target Rune, Effect Rune
    public string Rarity { get; set; } // "Common", "Uncommon", "Rare", "Mythic" 
    public bool IsSizing { get; set; } // Зависит ли эффект от объёма влитой маны?
}
