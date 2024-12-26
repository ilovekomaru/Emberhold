using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultConfig : MonoBehaviour
{
    public EntityStats entityStats;
}

[Serializable]
public struct EntityStats
{
    [Header("BASIC STATS")]
    public int hitPoints;
    public int speed;
    [Space]
    [Header("ATTACK")]
    public int damage;
    public int attackSpeed;
    public string attackAnimationName;
}