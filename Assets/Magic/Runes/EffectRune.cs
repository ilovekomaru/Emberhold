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

    public void ActivateRune(int givenMana, GameObject target)
    {
        if (givenMana >= ManaCost)
        {
            if (isSizing)
            {
                EffectWithSizing(target, givenMana);
            }
            else
            {
                Effect(target);
            }
        }
    }
    public virtual void Effect(GameObject target) { }
    public virtual void EffectWithSizing(GameObject target, int givenMana) { }
}
