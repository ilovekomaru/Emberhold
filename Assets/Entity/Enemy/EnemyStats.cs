using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float physMissChance;
    public float magiMissChance;

    public float speed;
    public float damage;

    private ParticleSystem missParticle;

    private void Awake()
    {
        
    }

    private void Update()
    {
        missParticle = this.GetComponent<ParticleSystem>();
        HealthCheck();
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
        Debug.Log("collision");
        if (collision!= null && collision.gameObject.tag == "Projectile")
        {
            Debug.Log("collision2");
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