using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void Execute(Vector3 pos)
    {
        Vector3 spawnPos = pos;
        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            spawnPos = hit.transform.position + Vector3.up*2;
        }
        this.transform.position = spawnPos;
    }
}
