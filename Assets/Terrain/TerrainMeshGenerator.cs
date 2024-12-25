using System;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class TerrainMeshGenerator : MonoBehaviour
{
    public static TerrainMeshGenerator Instance;

    public MeshFilter meshFilter;
    public TerrainMeshVariables meshVariables;
    public TerrainHeightmapVariables heightmapVariables;
    public BiomeVariables biomeVariables;
    public Gradient biomeGradient;
    public NavMeshSurface navMeshSurface;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnValidate()
    {
        GenerateMap();
    }

    public void Execute(bool b)
    {
        MeshCollider meshCollider;
        GenerateMap();
        meshCollider = this.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
        if(b)
            navMeshSurface.BuildNavMesh();
    }

    public void GenerateMap()
    {
        NativeArray<Color> _gradientColorArray = new NativeArray<Color>(100, Allocator.Persistent);

        for (int i = 0; i < 100; i++)
        {
            _gradientColorArray[i] = biomeGradient.Evaluate(i / 100f);
        }

        BiomeGenerator biomeGenerator = new BiomeGenerator(meshVariables, heightmapVariables, biomeVariables, _gradientColorArray);
        biomeGenerator.Schedule(meshVariables.TotalVerts, 10000).Complete();
        BiomeMaps _biomeMaps = biomeGenerator.ReturnAndDispose();
    
        HeightMapGenerator heightmapGenerator = new HeightMapGenerator(meshVariables, heightmapVariables, _gradientColorArray);
        heightmapGenerator.Schedule(meshVariables.TotalVerts, 10000).Complete();
        HeightMap _heightMap = heightmapGenerator.ReturnAndDispose();

        MeshGenerator meshGenerator = new MeshGenerator(meshVariables, _heightMap, _biomeMaps);
        meshGenerator.Schedule(meshVariables.terrainMeshDetail * meshVariables.terrainMeshDetail, 10000).Complete();

        meshFilter.mesh = meshGenerator.DisposeAndGetMesh();

        _gradientColorArray.Dispose();
    }
}

[BurstCompile]
public struct BiomeGenerator : IJobParallelFor
{
    [NativeDisableParallelForRestriction] private NativeArray<int> _biomeMap;
    private readonly TerrainMeshVariables _meshVariables;
    private readonly TerrainHeightmapVariables _heightmapVariables;
    private readonly BiomeVariables _biomeVariables;
    [NativeDisableParallelForRestriction] private NativeArray<Color> _gradient;
    [NativeDisableParallelForRestriction] private NativeArray<Color> _colMap;
    [NativeDisableParallelForRestriction] private int _baseRadius;
    [NativeDisableParallelForRestriction] private int _islandRadius;
    


    public BiomeGenerator(TerrainMeshVariables mv, TerrainHeightmapVariables hv, BiomeVariables bv, NativeArray<Color> grad)
    {
        _meshVariables = mv;
        _heightmapVariables = hv;
        _biomeVariables = bv;
        _biomeMap = new NativeArray<int>(_meshVariables.TotalVerts, Allocator.TempJob);
        _colMap = new NativeArray<Color>(_meshVariables.TotalVerts, Allocator.TempJob);
        _gradient = grad;
        _baseRadius = bv.baseRadius;
        _islandRadius = bv.islandRadius;
    }

    //Mathf.Clamp(Mathf.RoundToInt(BiomeNoise(pos, _biomeVariables.seed)), 0, 99);


    public void Execute(int threadIndex)
    {
        float x = threadIndex / /*(float)*/(_meshVariables.terrainMeshDetail + 1);
        float y = threadIndex % (_meshVariables.terrainMeshDetail + 1);
        float2 pos = new float2(x, y);
        int b = 0;
        int c = 0;

        if (isCenter(threadIndex))
        {
            b = 0;
            c = 0;
        }
        else if (isNearCenter(threadIndex))
        {
            b = Mathf.Clamp(Mathf.RoundToInt(BiomeNoise(pos, _biomeVariables.seed, 3f)), 0, 3);
            c = Mathf.Clamp(Mathf.RoundToInt(BiomeNoise(pos, _biomeVariables.seed, 99f)), 0, 99);
        }
        else
        {
            b = 4;
            c = 99;
        }

        _biomeMap[threadIndex] = b;
        _colMap[threadIndex] = _gradient[c];
    }

    public BiomeMaps ReturnAndDispose() => new BiomeMaps(_biomeMap, _colMap);
    public Color getColor(int index)
    {
        return _colMap[index];
    }

    float BiomeNoise(float2 pos, int seed, float amplitude)
    {
        float noiseVal = 0, freq = _biomeVariables.noiseScale;

        float v = (noise.snoise((pos + seed) / freq / _biomeVariables.biomeMeshDetail) + 1) / 2f;
        noiseVal += v * amplitude;
        return noiseVal;
    }


    bool isCenter(int index)
    {
        if (index < Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius))
            return false;
        if (index > Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius))
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius)
            return false;
        return true;
    }

    bool isNearCenter(int index)
    {
        if (index < Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _islandRadius))
            return false;
        if (index > Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _islandRadius))
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _islandRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _islandRadius)
            return false;
        return true;
    }
}

[BurstCompile]
public struct HeightMapGenerator : IJobParallelFor
{

    [NativeDisableParallelForRestriction,] private NativeArray<float> _heightMap;
    private readonly TerrainMeshVariables _meshVariables;
    private readonly TerrainHeightmapVariables _heightmapVariables;
    [NativeDisableParallelForRestriction] private NativeArray<Color> _gradient;
    [NativeDisableParallelForRestriction] private int _baseRadius;
    [NativeDisableParallelForRestriction] private float _baseHeight;
    [NativeDisableParallelForRestriction] private int _baseTransRadius;


    public HeightMapGenerator(TerrainMeshVariables mv, TerrainHeightmapVariables hv, NativeArray<Color> grad)
    {
        _meshVariables = mv;
        _heightmapVariables = hv;
        _heightMap = new NativeArray<float>(_meshVariables.TotalVerts, Allocator.TempJob);
        _gradient = grad;
        _baseRadius = hv.baseRadius;
        _baseHeight = hv.baseHeight;
        _baseTransRadius = hv.baseTransRadius;
    }

    public void Execute(int threadIndex)
    {
        float x = threadIndex / /*(float)*/(_meshVariables.terrainMeshDetail + 1);
        float y = threadIndex % (_meshVariables.terrainMeshDetail + 1);
        float2 pos = new float2(x, y);
        float h;

        if (isCenter(threadIndex))
        {
            h = _baseHeight;
        }
        else if (isNearCenter(threadIndex))
        {
            h = Mathf.Clamp(_baseHeight * (float)0.2 + Mathf.Clamp((OctavedSimplexNoise(pos, _heightmapVariables.seed) + OctavedRidgeNoise(pos, _heightmapVariables.seed)) / 2f * FalloffMap(pos) * _meshVariables.height, _heightmapVariables.waterLevel, 1000) * (float)0.8, _heightmapVariables.baseHeight - 10, _heightmapVariables.baseHeight + 10);
        }
        else
        {
            h = Mathf.Clamp((OctavedSimplexNoise(pos, _heightmapVariables.seed) + OctavedRidgeNoise(pos, _heightmapVariables.seed)) / 2f * FalloffMap(pos) * _meshVariables.height, _heightmapVariables.waterLevel, 1000);
        }


        _heightMap[threadIndex] = h / _meshVariables.TileEdgeLength;
    }

    public HeightMap ReturnAndDispose() => new HeightMap(_heightMap);


    bool isCenter(int index)
    {
        if (index < Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius))
            return false;
        if (index > Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius))
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius)
            return false;
        return true;
    }

    bool isNearCenter(int index)
    {
        if (index < Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius - _baseTransRadius))
            return false;
        if (index > Mathf.Sqrt(_meshVariables.TotalVerts) * (Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius + _baseTransRadius))
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) < Mathf.Sqrt(_meshVariables.TotalVerts) / 2 - _baseRadius - _baseTransRadius)
            return false;
        if (index % Mathf.Sqrt(_meshVariables.TotalVerts) > Mathf.Sqrt(_meshVariables.TotalVerts) / 2 + _baseRadius + _baseTransRadius)
            return false;
        return true;
    }

    float OctavedRidgeNoise(float2 pos, int seed)
    {
        float noiseVal = 0, amplitude = 1, freq = _heightmapVariables.noiseScale, weight = 1;

        for (int o = 0; o < _heightmapVariables.octaves; o++)
        {
            float v = 1 - Mathf.Abs(noise.snoise((pos + seed) / freq / _meshVariables.terrainMeshDetail));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * _heightmapVariables.weight);
            noiseVal += v * amplitude;

            freq /= _heightmapVariables.frequency;
            amplitude /= _heightmapVariables.lacunarity;
        }

        return noiseVal;
    }
    float OctavedSimplexNoise(float2 pos, int seed)
    {
        float noiseVal = 0, amplitude = 1, freq = _heightmapVariables.noiseScale;

        for (int o = 0; o < _heightmapVariables.octaves; o++)
        {
            float v = (noise.snoise((pos + seed) / freq / _meshVariables.terrainMeshDetail) + 1) / 2f;
            noiseVal += v * amplitude;

            freq /= _heightmapVariables.frequency;
            amplitude /= _heightmapVariables.lacunarity;
        }

        return noiseVal;
    }
    float FalloffMap(float2 pos)
    {
        float x = (pos.x / (_meshVariables.terrainMeshDetail + 1)) * 2 - 1;
        float y = (pos.y / (_meshVariables.terrainMeshDetail + 1)) * 2 - 1;

        float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));

        float a = _heightmapVariables.falloffSteepness;
        float b = _heightmapVariables.falloffOffset;

        return 1 - (Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow((b - b * value), a)));
    }
}

[BurstCompile]
public struct MeshGenerator : IJobParallelFor
{

    [NativeDisableParallelForRestriction] private NativeArray<Vector3> _verticies;
    [NativeDisableParallelForRestriction] private NativeArray<int> _triangleIndicies;
    private TerrainMeshVariables _meshVariables;
    private HeightMap _heightMap;
    private BiomeMaps _biomeMaps;

    public MeshGenerator(TerrainMeshVariables mv, HeightMap m, BiomeMaps b)
    {
        _meshVariables = mv;

        _verticies = new NativeArray<Vector3>(_meshVariables.TotalVerts, Allocator.TempJob);
        _triangleIndicies = new NativeArray<int>(_meshVariables.TotalTriangles, Allocator.TempJob);
        _heightMap = m;
        _biomeMaps = b;
    }

    public void Execute(int threadIndex)
    {
        int x = threadIndex / _meshVariables.terrainMeshDetail;
        int y = threadIndex % _meshVariables.terrainMeshDetail;

        // c - - - - d
        // |         |
        // |         |
        // |         |
        // a - - - - b
        // a is bottom left and the rest of the points are calculated using the index of a
        // we are only looping through each square to calculate the triangle and other bs

        int a = threadIndex + Mathf.FloorToInt(threadIndex / (float)_meshVariables.terrainMeshDetail);
        int b = a + 1;
        int c = b + _meshVariables.terrainMeshDetail;
        int d = c + 1;

        _verticies[a] = new Vector3(x + 0, _heightMap.Height[a], y + 0) * _meshVariables.TileEdgeLength;
        _verticies[b] = new Vector3(x + 0, _heightMap.Height[b], y + 1) * _meshVariables.TileEdgeLength;
        _verticies[c] = new Vector3(x + 1, _heightMap.Height[c], y + 0) * _meshVariables.TileEdgeLength;
        _verticies[d] = new Vector3(x + 1, _heightMap.Height[d], y + 1) * _meshVariables.TileEdgeLength;

        _triangleIndicies[threadIndex * 6 + 0] = a;
        _triangleIndicies[threadIndex * 6 + 1] = b;
        _triangleIndicies[threadIndex * 6 + 2] = c;
        _triangleIndicies[threadIndex * 6 + 3] = b;
        _triangleIndicies[threadIndex * 6 + 4] = d;
        _triangleIndicies[threadIndex * 6 + 5] = c;
    }

    public Mesh DisposeAndGetMesh()
    {
        // create and assign values to mesh
        var m = new Mesh();

        m.SetVertices(_verticies);
        m.SetColors(_biomeMaps.Color);
        m.triangles = _triangleIndicies.ToArray();

        m.RecalculateNormals();

        // Away with the memory hoarding!! (dispose the native arrays from memory)
        _verticies.Dispose();
        _triangleIndicies.Dispose();
        _heightMap.Dispose();
        _biomeMaps.Dispose();

        return m;
    }
}

[Serializable]
public struct TerrainMeshVariables
{
    [Range(1, 255)]
    public int terrainMeshDetail;
    public float terrainWidth;
    public float height;
    public int TotalVerts => (terrainMeshDetail + 1) * (terrainMeshDetail + 1);
    public int TotalTriangles => terrainMeshDetail * terrainMeshDetail * 6;
    public float TileEdgeLength => terrainWidth / terrainMeshDetail;
}
[Serializable]
public struct TerrainHeightmapVariables
{
    [Header("Noise")]
    public float noiseScale;
    public int seed;
    [Range(0, 4)]
    public float frequency, lacunarity;
    [Range(1, 10)]
    public int octaves;
    public float weight;

    public float falloffSteepness, falloffOffset;
    [Header("Extras")]
    public float waterLevel;
    public int baseRadius;
    public float baseHeight;
    public int baseTransRadius;
}
[Serializable]
public struct BiomeVariables
{
    public float noiseScale;
    public int seed;
    public float biomeMeshDetail;
    public int baseRadius;
    public int islandRadius;
}

public struct HeightMap
{
    [NativeDisableParallelForRestriction] public NativeArray<float> Height;

    public HeightMap(NativeArray<float> h)
    {
        Height = h;
    }

    public void Dispose()
    {
        Height.Dispose();
    }
}

public struct BiomeMaps
{
    [NativeDisableParallelForRestriction] public NativeArray<int> Biome;
    [NativeDisableParallelForRestriction] public NativeArray<Color> Color;

    public BiomeMaps(NativeArray<int> b, NativeArray<Color> c)
    {
        Biome = b;
        Color = c;
    }

    public void Dispose()
    {
        Biome.Dispose();
        Color.Dispose();
    }

}