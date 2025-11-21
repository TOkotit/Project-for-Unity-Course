using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private List<RoadTile> roadTiles = new();
    
    
    [SerializeField] private float roadSpeed = 10f;
    [SerializeField] private int zCoordinateThreshold = -15;
    [SerializeField] private int initialTilesCount = 8;
    
    private List<RoadTile> activeRoadTiles = new();
    private float currentRoadLength;
    
    
    public List<RoadTile> RoadTiles
    {
        get => roadTiles;
        set => roadTiles = value;
    }

    public float RoadSpeed
    {
        get => roadSpeed;
        set => roadSpeed = value;
    }

    public int ZCoordinateThreshold
    {
        get => zCoordinateThreshold;
        set => zCoordinateThreshold = value;
    }
    
    public void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentRoadLength = 0;
    
        for (var i = 0; i < initialTilesCount; i++)
        {
            var prefab = RoadTiles[Random.Range(0, RoadTiles.Count)]; 
            var pos = new Vector3(0, 0, currentRoadLength);
            var newTile = Instantiate(prefab, pos, Quaternion.identity); 
            activeRoadTiles.Add(newTile); 
            currentRoadLength += newTile.GetTileLength();
        }
    }

    public void FixedUpdate()
    {
        if (activeRoadTiles.Count == 0) return;

        var first = activeRoadTiles[0];
        if (first.transform.position.z < ZCoordinateThreshold)
        {
            var last = activeRoadTiles[^1];
        
            var lastMaxZ = last.GetWorldBounds().max.z;

            first.RecycleToZ(lastMaxZ);

            activeRoadTiles.RemoveAt(0);
            activeRoadTiles.Add(first);
        }
        
        foreach (var tile in activeRoadTiles)
            tile.SetPhysicsVelocity(RoadSpeed);
    }
    
}
