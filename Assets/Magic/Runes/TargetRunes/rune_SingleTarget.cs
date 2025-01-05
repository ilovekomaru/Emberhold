using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_SingleTarget : TargetRune
{
    public rune_SingleTarget()
    {
        Name = "Self-Target";
        Description = "Target for this spell is single enemy";
        ManaCost = 3;
        Type = "Target";
        Rarity = "Common";
        IsSizing = false;
    }
}
