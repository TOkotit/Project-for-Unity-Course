using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] public LevelStats00 LevelStats;
        
        [Header("Prefabs")]
        [SerializeField] private Enemy carPrefab;
        [SerializeField] private Enemy dronePrefab;
        private LevelModel _levelModel;
        
        private List<Enemy> _spawnedEnemyViews = new();
        public List<EnemySpot> EnemiesSpawnSpots = new()
        {
            new EnemySpot(new Vector3(-5, 0.1f, 0)),
            new EnemySpot(new Vector3(5, 0.1f, 0)),
            new EnemySpot(new Vector3(-5, 0.1f, 10)),
            new EnemySpot(new Vector3(5, 0.1f, 10)),
            new EnemySpot(new Vector3(0, 0.1f, 10)), 
        };

        public void Awake()
        {
            
            if (LevelStats is null)
                LevelStats = Resources.Load<LevelStats00>("Config/LevelStats00");
            
            if (Game.Instance != null)
            {
                _levelModel = Game.Instance.levelModel;
            }
            else
            {
                _levelModel.Initialize(LevelStats); 
            }
            
            
            SpawnNextWave();
        }
        
        public void SpawnNextWave()
        {
            var enemiesToSpawn = _levelModel.GetNextEnemyWave();

            if (enemiesToSpawn == null || enemiesToSpawn.Count == 0) return;

            foreach (var enemyModel in enemiesToSpawn)
            {
                var spot = GetFreeSpot();
                if (spot == null)
                {
                    Debug.LogError("Нет свободных мест для спавна!");
                    break;
                }

                var prefabToSpawn = enemyModel.EnemyType == EnemyType.Car ? carPrefab : dronePrefab;
                var newEnemyObject = Instantiate(prefabToSpawn, spot.Position, Quaternion.identity);
                newEnemyObject.Initialize(enemyModel);
                spot.IsFree = false; 
                _spawnedEnemyViews.Add(newEnemyObject);
            }
        }
        
        private EnemySpot GetFreeSpot()
        {
            foreach (var spot in EnemiesSpawnSpots)
            {
                if (spot.IsFree) return spot;
            }
            return null;
        }
    }
}