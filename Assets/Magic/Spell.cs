using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class Spell
{
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Rarity { get; set; } // "Common", "Uncommon", "Rare", "Mythic" 
    public bool IsSizing { get; set; } // Зависит ли эффект от объёма влитой маны?

    public GameObject Owner { get; set; }
    public List<CombatStats> Targets { get; set; }
    public Vector3 PointTarget { get; set; }

    public TargetRune TargetType { get; set; }
    public List<EffectRune> Effects { get; set; }
    public float[] ManaForSizingRunes { get; set; } // 4 позициb максимум; Проценты десятичной дробью, сумма - 1; сколько на каждую руну (из всех пяти) маны? Надо сделать поведение на случай, если руна не маштабируемая.

    public Spell(TargetRune targetType, List<EffectRune> effects, GameObject owner)
    {
        this.TargetType = targetType;
        this.Effects = effects;
        Owner = owner;
        Targets = new List<CombatStats>();

        ManaForSizingRunes = new float[] { 1 };
        
        // Spell's ManaCost defining
        foreach (var effect in effects)
        {
            ManaCost += effect.ManaCost;
        }
        ManaCost += targetType.ManaCost;
    }

    public void ActivateSpell(int givenMana)
    {
        if (givenMana >= ManaCost)
        {
            givenMana -= TargetType.ManaCost;

            //for (int i = 0; i < Effects.Count; i++)
            //{

            //}

            if (TargetType.GetType() == typeof(rune_SelfTarget))
            {
                Targets.Add(Owner.GetComponent<CombatStats>());
                EffectOfSpell(givenMana);
            }
            else if (TargetType.GetType() == typeof(rune_CircleAreaTarget))
            {
                // Не обращай внимания на название, пусть будет шар
                CheckIsOwnerIsTarget();
                EffectOfSpell(givenMana);
            }
            else if (TargetType.GetType() == typeof(rune_SquareAreaTarget))
            {
                // Не обращай внимания на название, пусть будет куб
                CheckIsOwnerIsTarget();
                EffectOfSpell(givenMana);
            }
        }
    }

    public void EffectOfSpell(int givenMana)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].ActivateRune((int)(givenMana * ManaForSizingRunes[i]), Targets, Owner.GetComponent<CombatStats>());
        }
    }

    public void CheckIsOwnerIsTarget()
    {
        for (int i = 0; i < Targets.Count; i++)
        {
            Targets[i] = Targets[i].gameObject == Owner ? null : Targets[i];
        }
    }
}
