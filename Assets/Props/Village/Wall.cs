using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int HP = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy's attack"))
        {
            //HP -= collision.gameObject.GetComponent<>();       //Enemy attack stats
        }
    }
}
