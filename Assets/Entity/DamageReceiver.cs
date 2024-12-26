using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    private DefaultConfig config;
    void Start()
    {
        config = GetComponent<DefaultConfig>();
    }

    void GetDamage(int damage)
    {
        if (config == null)
            return;

        if (damage > config.entityStats.hitPoints)
            config.entityStats.hitPoints = 0;
        else
            config.entityStats.hitPoints -= damage;
    }


    void Update()
    {
        
    }


}
