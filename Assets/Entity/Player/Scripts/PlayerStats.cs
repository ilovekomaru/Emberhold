using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;

    private MagicWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<MagicWeapon>();
    }

    private void Update()
    {
        
    }

    public void Heal(float hp)
    {
        health += hp;
    }

    private void Upgrade()  //todo
    {
        weapon.WeaponUpgrade();
    }

    private void OnCollisionEnter(Collision collision)  //todo
    {
        if (collision != null && collision.gameObject.tag.Equals("Enemy's attack"))
        {
            //health -= collision.gameObject.GetComponent<EnemyAttack>().damage;
        }
    }
}