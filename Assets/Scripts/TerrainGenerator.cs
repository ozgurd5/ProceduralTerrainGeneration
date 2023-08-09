using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TerrainGenerator : PerlinGeneratorBase
{
    public static TerrainGenerator Singleton;
    
    [Header("Assign - Tiles")]
    [SerializeField] private Tilemap terrainTilemap;
    [SerializeField] private List<Tile> terrainTileList;
    [SerializeField] private List<float> terrainTileRangeList;

    [Header("Assign - Noise Map")]
    public int seed;
    public int octaveNumber = 3;
    public float noiseScale;
    public Vector2Int offset;
    public float lacunarity;
    [Range(0f, 1f)] public float persistence;

    private Dictionary<Vector2Int, float> terrainNoiseMap;

    private void Awake()
    {
        Singleton = GetComponent<TerrainGenerator>();
        Random.InitState(seed);
        MovementManager.OnMovement += GenerateTerrain;
    }

    private void Update()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        terrainNoiseMap = GenerateNoiseMap(offset, octaveNumber, noiseScale, persistence, lacunarity);
        GenerateTiles(terrainTilemap, terrainNoiseMap, terrainTileList, terrainTileRangeList);
    }
}