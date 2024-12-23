using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class PropsGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    public RaycastSettings raySett;
    public PrefabVariation prefabVar;


    public void OnValidate()
    {
        Generate();
    }


    private void Start()
    {
        Generate();
    }


    public void Generate()
    {
        Clear();

        for (int i = 0; i < raySett.density; i++)
        {
            float sampleX = Mathf.Clamp(Mathf.PerlinNoise(i * raySett.seed, i * raySett.seed - 1), raySett.xRange.x, raySett.xRange.y);
            float sampleY = Mathf.Clamp(Mathf.PerlinNoise(i * raySett.seed, i * raySett.seed - 1), raySett.xRange.x, raySett.xRange.y);
            Vector3 rayStart = new Vector3(sampleX, raySett.maxHeight, sampleY);

            if(!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;

            if(hit.point.y < raySett.minHeight)
                continue;

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[i % prefabs.Count], transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, UnityEngine.Random.Range(prefabVar.rotationRange.x, prefabVar.rotationRange.y), Space.Self);
            instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), prefabVar.rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = new Vector3(
                UnityEngine.Random.Range(prefabVar.minScale.x, prefabVar.maxScale.x),
                UnityEngine.Random.Range(prefabVar.minScale.y, prefabVar.maxScale.y),
                UnityEngine.Random.Range(prefabVar.minScale.z, prefabVar.maxScale.z)
            );
        }
    }

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }


    [Serializable]
    public struct RaycastSettings
    {
        public int density;
        public int seed;
        [Space]
        public float minHeight;
        public float maxHeight;
        public Vector2 xRange;
        public Vector2 zRange;
    }
    [Serializable]
    public struct PrefabVariation
    {
        [Range(0, 1)]
        public float rotateTowardsNormal;
        public Vector2 rotationRange;
        public Vector3 minScale;
        public Vector3 maxScale;
    }
}
