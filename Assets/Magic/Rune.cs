using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Rune
{
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Type { get; set; } // "Defense", "Attack", "Buff", "Debuff"
    public string Rarity { get; set; } // "Common", "Uncommon", "Rare", "Mythic" 
    public bool isSizing { get; set; } // Зависит ли эффект от объёма влитой маны?

    public Rune()
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
