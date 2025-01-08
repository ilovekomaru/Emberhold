using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_SquareAreaTarget : TargetRune
{
    public rune_SquareAreaTarget(float sideSize)
    {
        Name = "Cube Area Target";
        Description = "Target is all in square area with casting-time-defined radius";
        ManaCost = (int)(4 * sideSize);
        Type = "Target";
        Rarity = "Uncommon";
        IsSizing = true;
    }
}
