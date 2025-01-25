using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int HP = 0;
    public bool isDestroyed;

    private void Update()
    {
        isDestroyed = HP <= 0;

        if (isDestroyed)
        {
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }

            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = true;
            }

            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy's attack"))
        {
            //HP -= collision.gameObject.GetComponent<>();       //Enemy attack stats
        }
    }
}
