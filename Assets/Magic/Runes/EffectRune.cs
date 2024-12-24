using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

public abstract class EffectRune : Rune
{
    public List<TargetRune> legalTargets;
    public EffectRune()
    {

    }

    public void ActivateRune(int givenMana, object target)
    {
        bool isLegal = false;
        var temp = target.GetType();

        foreach (var rune in legalTargets)
        {
            if (rune.GetType() == target.GetType())
            {
                isLegal = true;
                break;
            }
        }

        if (givenMana >= ManaCost)
        {
            if (isLegal)
            {
                if (temp == typeof(rune_SingleTarget))
                {
                    EffectSingleTarget(target as GameObject, givenMana);
                }
            }
        }
    }
    public virtual void EffectSingleTarget(GameObject target, int givenMana) { }
    //public virtual void EffectMultiTarget(GameObject[] target, int givenMana) { }
    //public virtual void EffectPointTarget(Vector3 target, int givenMana) { }
    public virtual void EffectAreaTarget(GameObject target, int givenMana) { } // GameObject?
    //public virtual void EffectForEachInAreaSingleTarget(GameObject target, int givenMana) { }
}
