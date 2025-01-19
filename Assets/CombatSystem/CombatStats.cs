using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    public int HP;
    public int maxHP;
    public int hpRecoveryPerSecond;
    public int shield;

    public int attack;
    public Item Weapon;
    public MagicWeapon MagicWeapon;

    public float MP;
    public float maxMP;
    public float mpRecoveryPerSecond;

    public float physicalResist;
    public float magicalResist;
    public float physicalBuffs;

    public List<Status> statuses;
    public List<float> timersOfStatusesLifes;

    public bool manualRecoveryPerSecond = false;
    float hpRestoreTimer;
    float mpRestoreTimer;
    float statusTimer;

    public void Start()
    {
        if (!manualRecoveryPerSecond)
        {
            hpRecoveryPerSecond = maxHP / 20;
            mpRecoveryPerSecond = maxMP / 100;
        }
    }

    public void Update()
    {
        for (int i = 0; i < timersOfStatusesLifes.Count; i++)
        {
            timersOfStatusesLifes[i] -= Time.deltaTime;
            if (timersOfStatusesLifes[i] <= 0)
            {
                timersOfStatusesLifes.RemoveAt(i);
                statuses.RemoveAt(i);
            }
        }

        statusTimer += Time.deltaTime;
        if (statusTimer >= 1)
        {
            statusTimer = 0;

            for (int i = 0; i < statuses.Count; i++)
            {
                statuses[i].Effect(this);
            }
        }

        if (HP <= maxHP)
        {
            hpRestoreTimer += Time.deltaTime;
            if (hpRestoreTimer >= 1)
            {
                hpRestoreTimer = 0;
                HP += hpRecoveryPerSecond;
            }
        }

        if (MP < maxMP)
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
        else if (damageType == "Falling")
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
            return;
        }

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

    public int CalculateDamageForOther(string damageType)
    {
        var damage = 0;

        if (damageType == "Physical")
        {
            damage = (attack + Weapon.GetComponent<Item>().stats.DamageToEnemies)*(int)(1 - physicalResist);
        }
        else if (damageType == "Magical")
        {
            damage = (attack + MagicWeapon.GetComponent<Item>().stats.DamageToEnemies) * (int)(1 - magicalResist);
        }

        return damage;
    }

    public void RestoreHP(int healValue)
    {
        HP += healValue;

        //if (HP > maxHP)
        //{
        //    HP = maxHP;
        //}
    }
}