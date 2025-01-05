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
        var spell = new Spell(new rune_SelfTarget(), new List<EffectRune> { new rune_Shield(), new rune_Damage()}, player);
        spell.ManaForSizingRunes = new float[] { .20f, .80f };
        print(spell.ManaCost);
        spell.ActivateSpell(100);
    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            var spell = new Spell(new rune_SelfTarget(), new List<EffectRune> { new rune_LargeHealing() }, player);
            spell.ActivateSpell(25);
        }
    }
}
