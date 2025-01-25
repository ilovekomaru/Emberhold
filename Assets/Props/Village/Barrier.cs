using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int HP = 0;
    public int nightNum = 0;
    public bool isNight = false;
    private void Update()
    {
        HP = !isNight ? 0 : HP;
        GetComponent<SphereCollider>().enabled = HP > 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy's attack"))
        //{
        //    // HP -= other.gameObject.GetComponent<EnemyStats>.damage;
        //}

        if (other.gameObject.GetComponents<ProjectileDamageStats>() != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
