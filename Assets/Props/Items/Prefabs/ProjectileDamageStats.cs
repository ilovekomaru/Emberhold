using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamageStats : MonoBehaviour
{
    public ProjectileDamage projectileDamage;
    public bool destructable = true;

    private void Awake()
    {
        Invoke("DestroyProjectile", 10f);
    }

    public void SetDamage(ProjectileDamage _damage)
    {
        projectileDamage.PhysDamage = _damage.PhysDamage;
        projectileDamage.MagiDamage = _damage.MagiDamage;
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}

[Serializable]
public struct ProjectileDamage
{
    public float PhysDamage;
    public float MagiDamage;
    //+ status effects
}