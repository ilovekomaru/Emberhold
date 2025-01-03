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

    public void ActivateRune(int givenMana, List<CombatStats> targets)
    {
        if (givenMana >= ManaCost)
        {
            if (IsSizing && givenMana > ManaCost)
            {
                foreach (CombatStats target in targets)
                {
                    EffectWithSizing(target, givenMana);
                }
            }
            else
            {
                foreach (CombatStats target in targets)
                {
                    Effect(target);
                }
            }
        }
    }
    public virtual void Effect(CombatStats target) { }
    public virtual void EffectWithSizing(CombatStats target, int givenMana) { }
}
