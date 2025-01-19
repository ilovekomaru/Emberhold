using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_PointTarget : TargetRune
{
    public rune_PointTarget()
    {
        Name = "Point Target";
        Description = "Target for this spell is point on the ground";
        ManaCost = 10;
        Type = "Target";
        Rarity = "Uncommon";
        IsSizing = false;
    }
}
