using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class LevelModel
    {
        private List<EnemyModel>  currentEnemiesOnLevelModels = new();
        public UnityEvent EnemiesWaveStarted = new();
        public UnityEvent EnemiesWaveFinished = new();
        
        public List<EnemyModel> EnemyPoolConfiguration { get; set; } = new();
        public List<int> WavesConfiguration { get; set; } = new();

        public List<EnemyModel> CurrentEnemiesOnLevelModels
        {
            get => currentEnemiesOnLevelModels;
            set
            {
                currentEnemiesOnLevelModels = value;
                if (currentEnemiesOnLevelModels != null && currentEnemiesOnLevelModels.Count == 0)
                {
                    EnemiesWaveFinished.Invoke();
                }
            }
        }
        
        public LevelModel() 
        {
            }
        public void Initialize(LevelStats00 _levelDataSO)
        {
            _levelDataSO.LoadIntoModel(this);
        }
        public List<EnemyModel> GetNextEnemyWave()
        {
            
            if (WavesConfiguration.Count == 0)
            {
                Debug.Log("Волны закончились!");
                return null;
            }
            
            var waveSize = WavesConfiguration[0];
            WavesConfiguration.RemoveAt(0);
            
            var waveEnemies = new List<EnemyModel>();
            
            for (var i = 0; i < waveSize; i++)
            {
                if (EnemyPoolConfiguration.Count > 0)
                {
                    var newEnemy = EnemyPoolConfiguration[0];
                    waveEnemies.Add(newEnemy);
                    EnemyPoolConfiguration.RemoveAt(0);
                    currentEnemiesOnLevelModels.Add(newEnemy);
                }
                else
                {
                    Debug.LogError("Ошибка конфигурации: Пул врагов закончился раньше, чем волны.");
                    break; 
                }
            }
            EnemiesWaveStarted.Invoke();
            return waveEnemies;
        }
        
        public void RemoveEnemy(EnemyModel enemy)
        {
            if(CurrentEnemiesOnLevelModels.Contains(enemy))
                CurrentEnemiesOnLevelModels.Remove(enemy);
            
            if (CurrentEnemiesOnLevelModels.Count == 0)
            {
                EnemiesWaveFinished.Invoke();
            }
        }
        
    }
}