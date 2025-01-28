using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public void NightStarts(int nightNumber)
    {
        for (int i = 0; i < nightNumber * 50; i++)
        {
            Instantiate(enemyPrefab, parent: this.gameObject.transform, position: );
        }
    }
}
