using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_Healing : EffectRune
{
    public rune_Healing()
    {
        Name = "Healing";
        Description = "Restores 15 HP";
        ManaCost = 10;
        Type = "Effect";
        Rarity = "Common";
        IsSizing = false;
    }

    public override void Effect(CombatStats target)
    {
        target.RestoreHP(15);
    }
}
