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
    public List<Rune> runes { get; set; }
    public List<EffectRune> effects { get; set; }
    public List<TargetRune> targets { get; set; }

    public Spell(string name, string description, List<Rune> runes)
    {
        Name = name;
        Description = description;
        this.runes = runes;

        foreach (var rune in runes)
        {
            ManaCost += rune.ManaCost;
        }
    }

    public Spell(string name, List<Rune> runes)
    {
        Name = name;
        this.runes = runes;
    }

    public void ActivateSpell(int givenMana)
    {
        if (givenMana >= ManaCost)
        {
            foreach
        }
    }
}
