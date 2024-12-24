using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Spell
{
    public int ManaCost { get; set; }
    public string Rarity { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool isSizing { get; set; }
    //public List<Rune> runes { get; set; }
    //public List<EffectRune> effects { get; set; }
    //public List<TargetRune> targets { get; set; }

    public Rune[] runes { get; set; }

    public Spell(string name, string description, Rune[] runes)
    {
        Name = name;
        Description = description;
        this.runes = runes;

        foreach (var rune in runes)
        {
            ManaCost += rune.ManaCost;
            if (rune.isSizing)
            {
                this.isSizing = true;
            }
        }
    }

    public Spell(string name, Rune[] runes)
    {
        Name = name;
        this.runes = runes;

        foreach (var rune in runes)
        {
            ManaCost += rune.ManaCost;
            if (rune.isSizing)
            {
                this.isSizing = true;
            }
        }
    }

    public void ActivateSpell(int givenMana)
    {
        if (givenMana >= ManaCost)
        {
            var effect = (EffectRune)runes[0];
            var target = (EffectRune)runes[1];

            effect.ActivateRune(givenMana, target);
        }
    }
}
