using Sydewa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ember : MonoBehaviour
{
    //public LightingManager DayNightManager;
    CombatStats EmberCombatStats;
    public bool isNight;

    void Start()
    {
        EmberCombatStats = GetComponent<CombatStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNight)
        {

        }
    }
}
