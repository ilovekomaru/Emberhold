using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_Fireball : EffectRune
{
    public rune_Fireball()
    {
        Name = "Fireball";
        Description = "";
        ManaCost = 30;
        Type = "Effect";
        Rarity = "Uncommon";
        IsSizing = true;
    }

    public override void Effect(CombatStats target, CombatStats spellOwner)
    {
        var damage = spellOwner.CalculateDamageForOther("Magical") + 50;
        target.DealDamageToThis(damage, "Magical");
    }

    public override void EffectWithSizing(CombatStats target, int givenMana, CombatStats spellOwner)
    {
        var damage = spellOwner.CalculateDamageForOther("Magical") + (50 * (givenMana/10));
        target.DealDamageToThis(damage, "Magical");
    }

}
