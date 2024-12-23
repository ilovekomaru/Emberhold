using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public float3 pos;

    public void SpawnPlayer()
    {
        Vector3 spawnPos = new Vector3(pos.x, pos.y, pos.z);
        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            spawnPos = hit.transform.position;
        }
        this.transform.position = spawnPos;
    }
}
