using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_Poison : EffectRune
{
    public rune_Poison()
    {
        Name = "Poison";
        Description = "Poisoned entity loses 5 HP every second";
        ManaCost = 20;
        Type = "Effect";
        Rarity = "Uncommon";
        IsSizing = false;
    }

    public override void Effect(CombatStats target, CombatStats spellOwner)
    {
        target.statuses.Add(new status_Poisoned());
        target.timersOfStatusesLifes.Add(new status_Poisoned().StatusTime);
    }

}
