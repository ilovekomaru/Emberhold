using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_CircleAreaTarget : TargetRune
{
    public float Radius { get; set; }
    public rune_CircleAreaTarget(float radius)
    {
        Name = "Self-Target";
        Description = "Target is all in circle area with casting-time-defined radius";
        ManaCost = (int)(2 * radius);
        Type = "Target";
        Rarity = "Common";
        IsSizing = true;
        Radius = radius;
    }
}
