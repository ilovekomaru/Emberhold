using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// Amber is actually VillageManager

public delegate void SupportSpellEffect();
public class Amber : MonoBehaviour
{
    public List<SupportSpell> supportSpells = new List<SupportSpell>()
    {
        new SupportSpell
        {
            Name = "Wall reconstruction",
            Description = "Use for it for 2 night times to restore walls",
            timeBetweenCasts = 100,
            Effect = new(WallReconstruction),
        },
        new SupportSpell
        {
            Name = "Healing field",
            Description = "Restores 50 HP every 15 seconds",
            timeBetweenCasts = 15,
            Effect = new(HealingField),
        },
    };

    public List<SupportSpell> activeSupportSpells = new List<SupportSpell>();

    public Wall Wall1;
    public Wall Wall2;
    public Wall Wall3;
    public Wall Wall4;

    public Barrier SanctuaryBarrier;
    public Barrier VillageBarrier;

    public static PlayerStats playerStats;

    public bool isNight = false;
    public int nightNumber = 0;

    public static float wallReconstructionTime = 0;

    public float timer1 = 0;
    public float timer2 = 0;
    public float timer3 = 0;

    private void Start()
    {
        Wall1.HP = 100 * (nightNumber/10 + 1);
        Wall2.HP = 100 * (nightNumber/10 + 1);
        Wall3.HP = 100 * (nightNumber/10 + 1);
        Wall4.HP = 100 * (nightNumber/10 + 1);

    //test
    activeSupportSpells.Add(supportSpells[0]);
    activeSupportSpells.Add(supportSpells[1]);
    }
    private void Update()
    {
        SanctuaryBarrier.isNight = isNight;
        VillageBarrier.isNight = isNight;

        //Night mode
        if (isNight)
        {
            //Timers for support spells
            timer1 += Time.deltaTime;
            if (timer1 >= activeSupportSpells[0].timeBetweenCasts)
            {
                timer1 = 0;

                activeSupportSpells[0].Effect();
            }

            timer2 += Time.deltaTime;
            if (timer2 > activeSupportSpells[1].timeBetweenCasts)
            {
                timer2 = 0;

                activeSupportSpells[1].Effect();
            }

            //timer3 += Time.deltaTime;
            //if (timer3 > activeSupportSpells[2].timeBetweenCasts)
            //{
            //    timer3 = 0;

            //    activeSupportSpells[2].Effect();
            //}
        }
    }

    // Support spells

    public static void HealingField()
    {
        playerStats.Heal(50);
    }

    public static void BuffingField()
    {

    }

    public static void WallReconstruction()
    {
        wallReconstructionTime += 1;
    }
    
    //Class methods
    public void NightStarts()
    {
        nightNumber++;
        isNight = true;
    }
    public void NightEnds()
    {
        isNight = false;

        if (wallReconstructionTime >= 2)
        {
            wallReconstructionTime = 0;

            Wall1.HP = 100 * (nightNumber / 10 + 1);
            Wall2.HP = 100 * (nightNumber / 10 + 1);
            Wall3.HP = 100 * (nightNumber / 10 + 1);
            Wall4.HP = 100 * (nightNumber / 10 + 1);
        }

        timer1 = 0;
        timer2 = 0;
        timer3 = 0;
    }
}

public struct SupportSpell
{
    public string Name, Description;
    public float timeBetweenCasts;
    public SupportSpellEffect Effect;
}