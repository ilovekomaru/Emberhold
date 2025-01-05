using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_LargeHealing : EffectRune
{
    public rune_LargeHealing()
    {
        Name = "Large healing";
        Description = "Restores 50 HP";
        ManaCost = 20;
        Type = "Effect";
        Rarity = "Uncommon";
        IsSizing = false;
    }

    public override void Effect(CombatStats target)
    {
        target.RestoreHP(50);
    }
}
