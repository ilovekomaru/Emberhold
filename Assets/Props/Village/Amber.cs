using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// Amber is actually VillageManager

public delegate void SupportSpellEffectForVillage();
public delegate void SupportSpellEffectForPlayer(PlayerStats player);
public class Amber : MonoBehaviour
{
    public List<SupportSpellForVillage> supportSpellsForVillage = new List<SupportSpellForVillage>()
    {
        new SupportSpellForVillage
        {
            Name = "Wall reconstruction",
            Description = "Use for it for 2 night times to restore walls",
            timeBetweenCasts = 100,
            Effect = new(WallReconstruction),
        },
    };
    public List<SupportSpellForPlayer> supportSpellsForPlayer = new List<SupportSpellForPlayer>()
    {
        new SupportSpellForPlayer
        {
            Name = "Healing field",
            Description = "Restores 50 HP each 15 seconds",
            timeBetweenCasts = 15,
            Effect = new(HealingField),
        },
    };

    public Wall Wall1;
    public Wall Wall2;
    public Wall Wall3;
    public Wall Wall4;

    public Barrier SanctuaryBarrier;
    public Barrier VillageBarrier;

    public PlayerStats PlayerStats;

    public bool isNight = false;
    public int nightNumber = 0;

    public static float wallReconstructionTime = 0;

    public float timer1 = 0;
    public float timer2 = 0;
    public float timer3 = 0;

    public SupportSpellForVillage supportSpellForVillage;
    public SupportSpellForPlayer supportSpellForPlayer;

    private void Start()
    {
        Wall1.HP = 100 * (nightNumber/10 + 1);
        Wall2.HP = 100 * (nightNumber/10 + 1);
        Wall3.HP = 100 * (nightNumber/10 + 1);
        Wall4.HP = 100 * (nightNumber/10 + 1);

        //test
        supportSpellForVillage = supportSpellsForVillage[0];
        supportSpellForPlayer = supportSpellsForPlayer[0];
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
            if (timer1 >= supportSpellForVillage.timeBetweenCasts)
            {
                timer1 = 0;

                supportSpellForVillage.Effect();
            }

            timer2 += Time.deltaTime;
            if (timer2 > supportSpellForPlayer.timeBetweenCasts)
            {
                timer2 = 0;

                supportSpellForPlayer.Effect(PlayerStats);
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

    public static void HealingField(PlayerStats player)
    {
        player.Heal(50);
        Debug.Log(player.health);
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

public struct SupportSpellForVillage
{
    public string Name, Description;
    public float timeBetweenCasts;
    public SupportSpellEffectForVillage Effect;
}

public struct SupportSpellForPlayer
{
    public string Name, Description;
    public float timeBetweenCasts;
    public SupportSpellEffectForPlayer Effect;
}