using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class LevelModel
    {
        private List<EnemyModel>  enemies;
        public UnityEvent EnemiesWaveStarted = new();
        public UnityEvent EnemiesWaveFinished = new();
        
        private LevelStats00 LevelDataSO = ScriptableObject.CreateInstance<LevelStats00>();

        
        public List<EnemyModel> EnemyPoolConfiguration { get; set; } = new()
        {
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone)
        };

        public List<int> WavesConfiguration { get; set; } = new() { 3, 2, 2 };

        public List<EnemyModel> CurrentEnemiesOnLevel
        {
            get => enemies;
            set
            {
                enemies = value;
                if (enemies.Count == 0)
                {
                    EnemiesWaveFinished.Invoke();
                }
            }
        }

        public LevelModel()
        {
            LevelDataSO.LoadIntoModel(this);
        }
        public List<EnemyModel> GetNextEnemyWave()
        {
            if (CurrentEnemiesOnLevel.Count == 0)
                return null;
            
            var waveSize = WavesConfiguration[0];
            WavesConfiguration.RemoveAt(0);
            
            var waveEnemies = new List<EnemyModel>();
            
            for (var i = 0; i < waveSize; i++)
            {
                if (EnemyPoolConfiguration.Count > 0)
                {
                    waveEnemies.Add(EnemyPoolConfiguration[0]);
                    EnemyPoolConfiguration.RemoveAt(0);
                }
                else
                {
                    UnityEngine.Debug.LogError("Ошибка конфигурации: Пул врагов закончился раньше, чем волны.");
                    break; 
                }
            }
            EnemiesWaveStarted.Invoke();
            return waveEnemies;
        }
        
    }
}