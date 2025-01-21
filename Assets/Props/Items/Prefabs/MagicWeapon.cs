using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(Item))]
public class MagicWeapon : MonoBehaviour
{
    public MagicWeaponStats stats;
    public List<Spell> spells;
    public int currentSpellID = 0;
    public float spellSwitchCooldown = 2f;

    Spell currentSpell;

    public int mana;

    public bool allowInvoke = true;
    bool shooting, readyToShoot;
    int bulletsShot;

    public Camera tpsCam;
    public Transform attackPoint;
    public GameObject muzzleFlash;

    public void Awake()
    {
        mana = stats.manapool;
        readyToShoot = true;
    }

    public void WeaponUpgrade()
    {
        stats.manapool = (int)(stats.manapool * stats.manapoolUpgradeMultiplyer);
        stats.physicDamageMultiplier += stats.physicDamageMultiplierUpgrade;
        stats.magicDamageMultiplier += stats.magicDamageMultiplierUpgrade;
    }

    private void Update()
    {
        CheckActiveSpell();
        AttackInput();
    }

    private void CheckActiveSpell()
    {
        currentSpell = spells[currentSpellID];
    }

    private void AttackInput()
    {
        if (currentSpell.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && mana < currentSpell.manaCost)
        {
            //playsound
        }

        if (readyToShoot && shooting && mana >= currentSpell.manaCost)
        {
            bulletsShot = 0;
            Shoot();
        }
    }
    private ProjectileDamage CalculateDamage()
    {
        ProjectileDamage damage = new ProjectileDamage();
        damage.PhysDamage = 1 * stats.physicDamageMultiplier * spells[currentSpellID].PhysDamage;
        damage.MagiDamage = 1 * stats.magicDamageMultiplier * spells[currentSpellID].MagiDamage;
        return damage;
    }

    private void Shoot()
    {
        readyToShoot = false;


        Ray ray = tpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(200f);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = UnityEngine.Random.Range(-currentSpell.spread, currentSpell.spread);
        float y = UnityEngine.Random.Range(-currentSpell.spread, currentSpell.spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y, 0f);

        GameObject currentBullet = Instantiate(currentSpell.prefab, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * currentSpell.shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * currentSpell.upwardForce, ForceMode.Impulse);
        
        ProjectileDamageStats currentBulletStats = currentBullet.AddComponent<ProjectileDamageStats>();
        currentBulletStats.SetDamage(CalculateDamage());

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        mana -= currentSpell.manaCost;
        bulletsShot++;

        if (allowInvoke)
        {
            allowInvoke = false;
            Invoke("ResetShot", currentSpell.timeBetweenShooting);
        }
        
        if(bulletsShot < currentSpell.bulletsPerTap && mana >= currentSpell.manaCost)
        {
            Invoke("Shoot", currentSpell.timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}


[Serializable]
public struct MagicWeaponStats
{
    public int manapool;
    public float manapoolUpgradeMultiplyer;

    public float physicDamageMultiplier;
    public float physicDamageMultiplierUpgrade;
    public float magicDamageMultiplier;
    public float magicDamageMultiplierUpgrade;
}

[Serializable]
public struct Spell
{
    //name corresponds with its functions name in Spells Class
    public string Name;
    public string Description;

    //stacer/bullet force
    public float shootForce, upwardForce;

    //weapon stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int manaCost, bulletsPerTap;
    public bool allowButtonHold;

    public int PhysDamage;
    public int MagiDamage;

    //is it accesible now (unlockable for player)
    public bool isUnlocked;
    public GameObject prefab;
}