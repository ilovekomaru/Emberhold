using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_SelfTarget : TargetRune
{
    public rune_SelfTarget()
    {
        Name = "Self-Target";
        Description = "Target for this spell is yourself";
        ManaCost = 5;
        Type = "Target";
        Rarity = "Common";
        IsSizing = false;
    }
}
