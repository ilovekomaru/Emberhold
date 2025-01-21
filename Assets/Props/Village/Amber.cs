using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Amber : MonoBehaviour
{
    public Wall Wall1;
    public Wall Wall2;
    public Wall Wall3;
    public Wall Wall4;

    public int Wall1HP;
    public int Wall2HP;
    public int Wall3HP;
    public int Wall4HP;

    public Barrier SanctuaryBarrier;
    public Barrier VillageBarrier;

    public bool isNight = false;

    private void Update()
    {
        Wall1HP = Wall1.HP;
        Wall2HP = Wall2.HP;
        Wall3HP = Wall3.HP;
        Wall4HP = Wall4.HP;

        //Night mode
        if (isNight)
        {

        }
    }

    // Support spells

    public void HealingField()
    {
        
    }

    public void BuffingField()
    {

    }

    public void WallReconstruction()
    {
        
    }
}