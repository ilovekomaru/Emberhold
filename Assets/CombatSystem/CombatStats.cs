using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    public int HP = 100;
    public int maxHP = 100;
    public int hpRecoveryPerSecond;
    public int shield = 0;

    public int attack = 10;

    public float MP = 10;
    public float maxMP = 10;
    public float mpRecoveryPerSecond;

    public float physicalResist = 0;
    public float magicalResist = 0;
    public float buffs = 0;

    public bool manualRecoveryPerSecond = false;
    float hpRestoreTimer = 0;
    float mpRestoreTimer = 0;

    private void Start()
    {
        if (!manualRecoveryPerSecond)
        {
            hpRecoveryPerSecond = maxHP / 20;
            mpRecoveryPerSecond = maxMP / 100;
        }
    }

    private void Update()
    {
        while (HP != maxHP)
        {
            hpRestoreTimer += Time.deltaTime;
            if (hpRestoreTimer >= 1)
            {
                hpRestoreTimer = 0;
                HP += hpRecoveryPerSecond;
            }
        }

        while (MP != maxMP)
        {
            mpRestoreTimer += Time.deltaTime;
            if (mpRestoreTimer >= 1)
            {
                mpRestoreTimer = 0;
                MP += mpRecoveryPerSecond;
            }
        }

        if (HP > maxHP)
        {
            HP = maxHP;
        }

        if (MP > maxMP)
        {
            MP = maxMP;
        }
    }
    public void DealDamageToThis(int damage, string damageType)
    {
        if (damageType == "Physical")
        {
            damage *= (int)(1 - physicalResist);
        }
        else if (damageType == "Magical")
        {
            damage *= (int)(1 - magicalResist);
        }

        if ((damageType != "Falling"))
        {
            if (shield > 0)
            {
                if (shield >= damage)
                {
                    shield -= damage;
                }
                else
                {
                    damage -= shield;
                    shield = 0;
                    HP -= damage;
                }
            }
            else
            {
                HP -= damage;

                if (HP < 0)
                {
                    HP = 0;
                }
            }
        }
        else
        {
            HP -= damage;
        }
    }

    public int CalculateDamageForOther()
    {
        return attack * (int)(1 + buffs);
    }

    public void RestoreHP(int healValue)
    {
        HP += healValue;

        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }
}
