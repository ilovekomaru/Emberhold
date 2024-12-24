using System;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using TMPro;

public class SandTerrainGenerator : MonoBehaviour
{
    public static SandTerrainGenerator Instance;

    public MeshFilter meshFilter;
    public SandHeightMapVariables sandHeightMapVariables;
    public TerrainMeshVariables meshVariables;


    private MeshCollider meshCollider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnValidate()
    {
        GenerateMap();
    }

    private void Start()
    {
        GenerateMap();
        this.AddComponent<MeshCollider>();
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
    }

    public void GenerateMap()
    {
        Color sandColor = Color.yellow;

        BiomeMapsFill biomeMapsFill = new BiomeMapsFill(sandHeightMapVariables, meshVariables);
        biomeMapsFill.Schedule(meshVariables.TotalVerts, 10000).Complete();
        BiomeMaps _biomeMaps = biomeMapsFill.ReturnAndDispose();

        SandHeightMapGenerator sandHeightMapGenerator = new SandHeightMapGenerator(sandHeightMapVariables, meshVariables);
        sandHeightMapGenerator.Schedule(meshVariables.TotalVerts, 10000).Complete();
        HeightMap _heightMap = sandHeightMapGenerator.ReturnAndDispose();

        MeshGenerator sandMeshGenerator = new MeshGenerator(meshVariables, _heightMap, _biomeMaps);
        sandMeshGenerator.Schedule(meshVariables.terrainMeshDetail* meshVariables.terrainMeshDetail, 10000).Complete();

        meshFilter.mesh = sandMeshGenerator.DisposeAndGetMesh();
    }
}

public struct SandHeightMapGenerator : IJobParallelFor
{
    private readonly SandHeightMapVariables _heightmapVariables;
    private readonly TerrainMeshVariables _meshVariables;
    [NativeDisableParallelForRestriction,] private NativeArray<float> _heightMap;
    public SandHeightMapGenerator(SandHeightMapVariables sv, TerrainMeshVariables mv)
    {
        _heightmapVariables = sv;
        _meshVariables = mv;
        _heightMap = new NativeArray<float>(_meshVariables.TotalVerts, Allocator.TempJob);
    }

    public void Execute(int threadIndex)
    {
        float x = threadIndex / /*(float)*/(_meshVariables.terrainMeshDetail + 1);
        float y = threadIndex % (_meshVariables.terrainMeshDetail + 1);
        float2 pos = new float2(x, y);
        float h;

        if (isIsland(threadIndex))
        {
            h = 0;
        }
        else
        {
            h = -10;
        }

        _heightMap[threadIndex] = h;
    }

    public HeightMap ReturnAndDispose() => new HeightMap(_heightMap);

    bool isIsland(int index)
    {
        if (index < Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _heightmapVariables.islandRadius))
            return false;
        if (index > Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _heightmapVariables.islandRadius))
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _heightmapVariables.islandRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _heightmapVariables.islandRadius)
            return false;
        return true;
    }
}

public struct BiomeMapsFill : IJobParallelFor
{
    private readonly TerrainMeshVariables _meshVariables;
    private readonly SandHeightMapVariables _heightmapVariables;
    [NativeDisableParallelForRestriction] private NativeArray<int> _biomeMap;
    [NativeDisableParallelForRestriction] private NativeArray<Color> _colMap;

    public BiomeMapsFill(SandHeightMapVariables sv, TerrainMeshVariables mv)
    {
        _heightmapVariables = sv;
        _meshVariables = mv;
        _biomeMap = new NativeArray<int>(_meshVariables.TotalVerts, Allocator.TempJob);
        _colMap = new NativeArray<Color>(_meshVariables.TotalVerts, Allocator.TempJob);
    }

    public void Execute(int threadIndex)
    {
        int b = 0;
        if (isIsland(threadIndex))
        {
            b = 0;
        }
        else
        {
            b = 4;
        }

        _biomeMap[threadIndex] = b;
        _colMap[threadIndex] = Color.yellow;
    }

    public BiomeMaps ReturnAndDispose() => new BiomeMaps(_biomeMap, _colMap);

    bool isIsland(int index)
    {
        if (index < _meshVariables.TotalVerts / 2 - _heightmapVariables.islandRadius)
            return false;
        if (index > _meshVariables.TotalVerts / 2 + _heightmapVariables.islandRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _heightmapVariables.islandRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _heightmapVariables.islandRadius)
            return false;
        return true;
    }
}


[Serializable]
public struct SandHeightMapVariables
{
    public int islandRadius;
    public float grad;
}