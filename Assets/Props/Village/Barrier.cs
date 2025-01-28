using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int HP = 1;
    public bool isNight = false;

    private void Update()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = HP > 0;
        gameObject.GetComponent<SphereCollider>().enabled = isNight && HP > 0;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isNight && other.gameObject.CompareTag("Enemy's attack"))
        {
            HP -= (int)other.gameObject.GetComponent<ProjectileDamageStats>().projectileDamage.MagiDamage;
            Destroy(other.gameObject);
        }
    }
}
