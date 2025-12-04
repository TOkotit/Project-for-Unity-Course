using UnityEngine;
using System.Collections.Generic;
using Road_scripts;

public class RoadGenerator : MonoBehaviour
{
    [Header("Данные Конфигурации")]
    [SerializeField] private RoadStatsSO roadStats; 
    
    [Header("Префабы (Рандомизация)")]
    [SerializeField] private List<RoadTile> roadTilePrefabs = new(); 

    private float tileLength; 
    private List<RoadTile> activeRoadTiles = new();

    public void Awake()
    {
        
        if (!roadStats || roadTilePrefabs.Count == 0)
        {
            if (!roadStats) Debug.LogError("RoadGenerator: RoadStatsSO не назначен!");
            if (roadTilePrefabs.Count == 0) Debug.LogError("RoadGenerator: Список префабов дорожных плиток пуст!");
            return;
        }
        
        tileLength = MeasureTileLength(roadTilePrefabs[0]);
        
        if (roadStats.visibleTilesCount <= 1)
        {
            Debug.LogWarning("visibleTilesCount слишком мал. Установлено минимальное значение: 2.");
            roadStats.visibleTilesCount = 2;
        }
        
        SpawnInitialRoad();
    }

    private static float MeasureTileLength(RoadTile tilePrefab)
    {
        var renderer = tilePrefab.GetComponent<Renderer>();

        if (renderer) return renderer.bounds.size.z;
        
        Debug.LogError($"Тайл {tilePrefab.name} не имеет компонента Renderer! Невозможно измерить длину.");
        return 20f;
    }

    private void SpawnInitialRoad()
    {
        var nextZPosition = 0f; 

        for (var i = 0; i < roadStats.visibleTilesCount; i++)
        {
            SpawnTile(nextZPosition);
            nextZPosition += tileLength; 
        }
    }

    private void SpawnTile(float zPos)
    {
        var prefab = roadTilePrefabs[Random.Range(0, roadTilePrefabs.Count)];
        var newTile = Instantiate(prefab, transform);
        
        newTile.transform.position = new Vector3(0, 0, zPos);
        activeRoadTiles.Add(newTile);
    }
    
    public void Update()
    {
        foreach (var tile in activeRoadTiles)
            tile.Move(roadStats.roadSpeed); 

        if (activeRoadTiles.Count <= 0) return;
        var firstTile = activeRoadTiles[0];

        if (firstTile.transform.position.z < roadStats.recycleThresholdZ)
            RecycleTile(firstTile);
    }

    private void RecycleTile(RoadTile tile)
    {
        var lastTile = activeRoadTiles[^1];
        
        var newZ = lastTile.transform.position.z + tileLength; 

        tile.transform.position = new Vector3(0, 0, newZ);

        activeRoadTiles.RemoveAt(0);
        activeRoadTiles.Add(tile);
    }
}