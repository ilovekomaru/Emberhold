using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_VerticalWindZone : EffectRune
{
    public rune_VerticalWindZone()
    {
        Name = "Vertical Wind Zone";
        Description = "Send all enemies in area to the air. Distance depends on given mana.";
        ManaCost = 50;
        Type = "Effect";
        Rarity = "Rare";
        IsSizing = true;
    }

    public override void Effect(CombatStats target, CombatStats spellOwner)
    {
        target.gameObject.transform.position = Vector3.up * 5;
    }

    public override void EffectWithSizing(CombatStats target, int givenMana, CombatStats spellOwner)
    {
        target.gameObject.transform.position = Vector3.up * (5 + (givenMana/10));
    }
}
