using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class rune_Shield : EffectRune
{
    public rune_Shield()
    {
        Name = "Shield";
        Description = "Makes shield which strength depends on given for this rune mana";
        ManaCost = 5;
        Type = "Defense"; 
        Rarity = "Common";
        isSizing = true;
    }

    public override void Effect(GameObject target)
    {
        target.GetComponent<CombatStats>().shield = 25; 
    }
    public override void EffectWithSizing(GameObject target, int givenMana)
    {
        target.GetComponent<CombatStats>().shield = 5*givenMana;
    }
}
