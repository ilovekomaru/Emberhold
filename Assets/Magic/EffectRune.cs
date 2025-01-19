using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EffectRune : Rune
{
    public EffectRune()
    {

    }

    public void ActivateRune(int givenMana, List<CombatStats> targets, CombatStats spellOwner)
    {
        if (givenMana >= ManaCost)
        {
            if (IsSizing && givenMana > ManaCost)
            {
                foreach (CombatStats target in targets)
                {
                    EffectWithSizing(target, givenMana, spellOwner);
                }
            }
            else
            {
                foreach (CombatStats target in targets)
                {
                    Effect(target, spellOwner);
                }
            }
        }
    }
    public virtual void Effect(CombatStats target, CombatStats spellOwner) { }
    public virtual void EffectWithSizing(CombatStats target, int givenMana, CombatStats spellOwner) { }
}
