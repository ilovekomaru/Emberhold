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
        Type = "Effect"; 
        Rarity = "Common";
        IsSizing = true;
    }

    public override void Effect(CombatStats target, CombatStats spellOwner)
    {
        target.shield = 25; 
    }
    public override void EffectWithSizing(CombatStats target, int givenMana, CombatStats spellOwner)
    {
        target.shield = 5*givenMana;
    }

}
