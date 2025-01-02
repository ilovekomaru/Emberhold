using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class main : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    void Start()
    {
        var spell = new Spell(new rune_SelfTarget(), new List<EffectRune> { new rune_Shield() }, player);
        spell.ManaForSizingRunes = new float[] { 1 };
        print(spell.ManaCost);
        spell.ActivateSpell(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
