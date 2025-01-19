using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
    public float StatusTime { get; set; }
    public bool isActivated { get; set; }

    public Status()
    {
        
    }

    public virtual void Effect(CombatStats target) { }
}

//Statuses
public class status_Poisoned : Status
{
    public status_Poisoned()
    {
        StatusTime = 5;
        isActivated = false;
    }

    public override void Effect(CombatStats target)
    {
        if (!isActivated)
        {
            target.HP -= 5;
        }
    }
}