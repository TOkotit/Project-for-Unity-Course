using UnityEngine;
using System.Collections.Generic;
using Levels;
using Road_scripts;
using UnityEngine.Serialization;

public class RoadGenerator : MonoBehaviour
{
    public RoadStatsSO RoadStatsSO = ScriptableObject.CreateInstance<RoadStatsSO>(); 
    
    public List<RoadTile> roadTilePrefabs;

    public LevelStats00 LevelStats = ScriptableObject.CreateInstance<LevelStats00>();
    public float RoadSpeed;
    public int VisibleTilesCount;
    public float RecycleThresholdZ;
    private float tileLength; 
    private List<RoadTile> activeRoadTiles = new();

    public void Awake()
    {
        RoadStatsSO.LoadIntoModel(this);
        
        tileLength = MeasureTileLength(roadTilePrefabs[0]);
        
        if (VisibleTilesCount <= 1)
        {
            Debug.LogWarning("visibleTilesCount слишком мал. Установлено минимальное значение: 2.");
            VisibleTilesCount = 2;
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

        for (var i = 0; i < VisibleTilesCount; i++)
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
            tile.Move(RoadSpeed); 

        if (activeRoadTiles.Count <= 0) return;
        var firstTile = activeRoadTiles[0];

        if (firstTile.transform.position.z < RecycleThresholdZ)
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