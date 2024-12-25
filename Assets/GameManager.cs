using System;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;

public class StartGame : MonoBehaviour
{
    [SerializeField] PlayerSpawn playerSpawn;
    public PlayerSpawnVariables playerSpawnVariables;
    public SeedValues seedValues;

    public TerrainMeshGenerator terrainMeshGenerator;
    public PropsGenerator propsGenerator;
    public bool generateNavMesh;
    

    private void Start()
    {
        Execute();
    }

    public void Execute()
    {
        seedValues.seed = UnityEngine.Random.Range(0, 9999999);
        terrainMeshGenerator.heightmapVariables.seed = seedValues.seed;
        terrainMeshGenerator.biomeVariables.seed = seedValues.seed;

        terrainMeshGenerator.Execute(generateNavMesh);
        propsGenerator.Execute();
        playerSpawn.Execute(playerSpawnVariables.pos);
    }


    void Update()
    {

    }
}


[Serializable]
public struct PlayerSpawnVariables
{
    public Vector3 pos;
}

[Serializable]
public struct SeedValues
{
    public int seed;
}