using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float ModelHeight;
    public float ModelWidth;

    public float health;
    public float physMissChance;
    public float magiMissChance;

    public float speed;
    public float damage;

    public GameObject missPrefab;

    private ParticleSystem missParticle;

    private void Awake()
    {
        Invoke("MissPrefab", 1f);
    }

    private void Update()
    {
        HealthCheck();
    }

    private void MissPrefab()
    {
        missParticle = Instantiate(missPrefab, this.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision!= null && collision.gameObject.tag == "Projectile")
        {
            if(UnityEngine.Random.Range(0, 100) > physMissChance)
            {
                health -= collision.gameObject.GetComponent<ProjectileDamage>().PhysDamage;
            }
            else
            {
                PlayMissParticle(true);
            }
            if (UnityEngine.Random.Range(0, 100) > magiMissChance)
            {
                health -= collision.gameObject.GetComponent<ProjectileDamage>().MagiDamage;
            }
            else
            {
                PlayMissParticle(false);
            }
            if (collision.gameObject.GetComponent<ProjectileDamageStats>().destructable)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void PlayMissParticle(bool isPhys)
    {
        Color particleColor = Color.white;
        if(isPhys)
        {
            particleColor = Color.yellow;
        }
        else
        {
            particleColor = Color.cyan;
        }
        missParticle.main.startColor.Equals(particleColor);
        missParticle.Emit(1);
    }
}