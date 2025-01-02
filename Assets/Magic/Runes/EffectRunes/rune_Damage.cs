using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_Damage : EffectRune
{
    public rune_Damage()
    {
        Name = "Damage";
        Description = "Deals damage to target which depends on given mana for this rune";
        ManaCost = 10;
        Type = "Effect";
        Rarity = "Rare";
        IsSizing = true;
    }

    public override void Effect(CombatStats target)
    {
        target.DealDamageToThis(20, "Magical");
    }
    public override void EffectWithSizing(CombatStats target, int givenMana)
    {
        target.DealDamageToThis(givenMana * 2, "Magical");
    }
}
