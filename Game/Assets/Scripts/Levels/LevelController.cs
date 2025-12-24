using Assets.Scripts.Levels;
using Entities.PlayerScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] public LevelStats00 LevelStats;
        [SerializeField] public CarEnemySO CarEnemyStats;
        [SerializeField] public DroneEnemySO DroneStats;
        
        
        [Header("Prefabs")]
        [SerializeField] private Enemy carPrefab;
        [SerializeField] private Enemy dronePrefab;
        [SerializeField] private PlayerController playerPrefab;
        private LevelModel _levelModel; // +
        
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
            if (LevelStats == null)
                LevelStats = Resources.Load<LevelStats00>("Config/Level1StatsSO");
            if (CarEnemyStats == null)
                CarEnemyStats = Resources.Load<CarEnemySO>("Config/CarEnemyStatsSO");
            if (DroneStats == null)
                DroneStats = Resources.Load<DroneEnemySO>("Config/DroneEnemyStatsSO");
            
            if (Game.Instance != null)
            {
                _levelModel = Game.Instance.LevelModel;
                _levelModel.Initialize(LevelStats);
            }
            if (_levelModel == null)
            {
                Debug.LogError("LevelController: LevelModel не найден в Game.Instance! " +
                               "Проверьте Script Execution Order для GameplayEntryPoint.");
                return;
            }
            _levelModel.LevelCompleted.AddListener(OnLevelCompleted); //+
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            
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
                
                if (enemyModel.EnemyType == EnemyType.Car)
                {
                    enemyModel.LoadStatsFromSO(CarEnemyStats); 
                }
                else if (enemyModel.EnemyType == EnemyType.Drone)
                {
                    enemyModel.LoadStatsFromSO(DroneStats);
                }

                Vector3 positionToSpawn;
                
                var prefabToSpawn = enemyModel.EnemyType == EnemyType.Car ? carPrefab : dronePrefab;
                if (enemyModel.EnemyType == EnemyType.Drone)
                {
                    positionToSpawn = spot.Position + Vector3.up * 6;
                }
                else
                {
                    positionToSpawn = spot.Position;
                }
                
                var newEnemyObject = Instantiate(prefabToSpawn, positionToSpawn, Quaternion.identity);
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

        private void OnLevelCompleted(string levelId) // +
        {
            float time = Time.time;
            Debug.Log($"{levelId} пройден за {time:F2} сек");

            LevelProgressManager.Instance.CompleteLevel(levelId, time);
        }

        private void OnDestroy() // +
        {
            if (_levelModel != null)
                _levelModel.LevelCompleted.RemoveListener(OnLevelCompleted);
        }
    }
}