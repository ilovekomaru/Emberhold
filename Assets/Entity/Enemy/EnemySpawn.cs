
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(CapsuleCollider), typeof(NavMeshAgent))]
public class EnemySpawn : MonoBehaviour
{
    public List<SpawnerSpawnBoundaries> spawnBoundaries;

    public void Spawn(int spawner)
    {
        SetupCollider();
        float posX = UnityEngine.Random.Range(spawnBoundaries[spawner].SpawnRangeX.x, spawnBoundaries[spawner].SpawnRangeX.y);
        float posY = UnityEngine.Random.Range(spawnBoundaries[spawner].SpawnRangeY.x, spawnBoundaries[spawner].SpawnRangeY.y);
        Spawn(new Vector2(posX, posY));
    }

    public void Spawn(Vector2 pos)
    {
        SetupCollider();
        Vector3 spawnPos = new Vector3(pos.x, 300f, pos.y);
        Debug.DrawLine(spawnPos, spawnPos + Vector3.down*300);
        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            spawnPos = hit.transform.position;
        }
        this.transform.position = spawnPos;
    }

    private void SetupCollider()
    {
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();

        float height = enemyStats.ModelHeight;
        float width = enemyStats.ModelWidth;

        collider.center = new Vector3(0f, height/2, 0f);
        collider.radius = width/2;
        collider.height = height;
    }
}

[Serializable]
public struct SpawnerSpawnBoundaries
{
    public Vector2 SpawnRangeX;
    public Vector2 SpawnRangeY;
}