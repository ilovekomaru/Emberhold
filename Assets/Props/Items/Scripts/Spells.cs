using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public static Spells Instance;
    public List<Spell> spell = new List<Spell>();

    public void ActivateSpell(Spell spell)
    {
        if (spell.Name == "LazerBeam")
        {
            
        }
    }
}


[Serializable]
public struct Spell
{
    //name corresponds with its functions name in Spells Class
    public string Name;
    public string Description;
    //is it accesible now (unlockable for player)
    public bool isUnlocked;
}