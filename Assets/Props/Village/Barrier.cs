using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int HP = 0;
    public bool isNight = false;
    private void Update()
    {
        GetComponent<SphereCollider>().enabled = isNight;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Material color: 898989 => DCDCDC => wait ~0.5s => 898989

        //if (collision.gameObject.CompareTag("Enemy's attack"))
        //{
        //    gameObject.GetComponent<MeshRenderer>().material.color = new Color(220, 220, 220, 50);
        //    gameObject.GetComponent<MeshRenderer>().material.color = new Color(137, 137, 137, 50);
        //}

        /*else*/if (collision.gameObject.CompareTag("Player") && gameObject.name == "VillageBarrier")
        {
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<SphereCollider>().enabled = true;
    }
}
