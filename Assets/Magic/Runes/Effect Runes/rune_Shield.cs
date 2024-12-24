using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        legalTargets = new List<TargetRune>()
        {
            new rune_SingleTarget()
        };
    }

    public override void EffectSingleTarget(GameObject target, int givenMana)
    {
        if (givenMana == ManaCost)
        {
            target.GetComponent<CombatStats>().shield = 25;
        }
        else if (givenMana > ManaCost && isSizing == true)
        {
            target.GetComponent<CombatStats>().shield = 5 * givenMana;
        }
    }
}
