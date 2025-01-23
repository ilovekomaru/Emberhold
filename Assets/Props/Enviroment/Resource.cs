using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceStats stats;

    public GameObject prefab;
    public GameObject missPrefab;

    private GameObject particleHolder;
    private ParticleSystem particles;
    private ParticleSystem missParticle;

    private void Awake()
    {
        particleHolder = Instantiate(prefab, transform);
        particles = particleHolder.GetComponent<ParticleSystem>();
        missParticle = Instantiate(missPrefab, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    public void Mine(int axeDamage, int pickaxeDamage, Vector3 hitPosition, out int wood, out int stone)
    {
        wood = 0;
        stone = 0;
        if (axeDamage >= stats.minDamage && stats.isTree)
        {
            stats.health -= axeDamage;
            wood += stats.resourceCount;
            EmitParticles(hitPosition, 15);
        }
        else if(pickaxeDamage >= stats.minDamage && !stats.isTree)
        {
            stats.health -= pickaxeDamage;
            stone += stats.resourceCount;
            EmitParticles(hitPosition, 15);
        }
        else
        {
            PlayMissParticle(hitPosition);
        }

        CheckHealth();
    }

    private void CheckHealth()
    {
        Debug.Log(stats.health);
        if (stats.health <= 0)
        {
            Invoke("DestroyResource", 1f);
        }
    }

    private void EmitParticles(Vector3 _hitPos, int count)
    {
        particleHolder.transform.position = _hitPos;
        particles.Emit(count);
    }
    
    private void DestroyResource()
    {
        Destroy(this.gameObject);
    }

    private void PlayMissParticle(Vector3 _hitPosition)
    {
        Color particleColor = Color.white;
        missParticle.gameObject.transform.LookAt(_hitPosition);
        missParticle.gameObject.transform.position = _hitPosition;
        missParticle.main.startColor.Equals(particleColor);
        missParticle.Emit(1);
    }
}

[Serializable]
public struct ResourceStats
{
    public int health;
    public int minDamage;
    public bool isTree;
    public int resourceCount;
}