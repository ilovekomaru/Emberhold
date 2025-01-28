using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int collisions;
    public GameObject enemyPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if (collisions > 25)
        {
            GameOver();
        }
    }

    private void GameOver()
    {

    }
}
