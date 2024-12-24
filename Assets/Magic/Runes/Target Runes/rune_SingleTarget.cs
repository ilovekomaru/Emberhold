using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rune_SingleTarget : TargetRune
{
    public rune_SingleTarget()
    {
        Name = "Single target";
        Description = "Makes shield which strength depends on given for this rune mana";
        ManaCost = 5;
        Type = "Target";
        Rarity = "Common";
    }
}
