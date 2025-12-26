using Assets.Scripts.Levels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class LevelModel
    {
        private List<EnemyModel>  currentEnemiesOnLevelModels = new();
        private BuffSystemModel buffSystemModel;
        public UnityEvent EnemiesWaveStarted = new();
        public UnityEvent EnemiesWaveFinished = new();
        public UnityEvent<string> LevelCompleted = new();
        public List<int> WavesConfiguration { get; set; } = new();
        
        public LevelModel() 
        {
            
        }
        public void Initialize(LevelStats00 _levelDataSO)
        {
            if (_levelDataSO == null)
            {
                Debug.LogError("LevelModel: Попытка инициализации NULL конфигом!");
                return;
            }

            buffSystemModel = Game.Instance.BuffModel;
            _levelDataSO.LoadIntoModel(this);
        }
        public List<EnemyModel> GetNextEnemyWave()
        {
            if (WavesConfiguration.Count == 0)
            {
                Debug.Log("Волны закончились! Уровень пройден!"); // + 
                LevelCompleted?.Invoke(Game.Instance.CurrentLevelId); // + 
                return null;
            }
            
            currentEnemiesOnLevelModels.Clear();
            
            var waveSize = WavesConfiguration[0];
            WavesConfiguration.RemoveAt(0);
            
            var waveEnemies = new List<EnemyModel>();
            
            for (var i = 0; i < waveSize; i++)
            {
                var randomType = (EnemyType)Random.Range(0, 2);
                var newEnemy = new EnemyModel(randomType);
                newEnemy.InitializePointsReward(buffSystemModel);
                waveEnemies.Add(newEnemy);
                
                currentEnemiesOnLevelModels.Add(newEnemy);
            }
            EnemiesWaveStarted.Invoke();
            return waveEnemies;
        }
        
        public void RemoveEnemy(EnemyModel enemy)
        {
            
            if (currentEnemiesOnLevelModels.Contains(enemy))
            {
                currentEnemiesOnLevelModels.Remove(enemy);
            }
            currentEnemiesOnLevelModels.RemoveAll(e => e == null);
            Debug.Log($"[LevelModel] Враг удален. Осталось в списке: {currentEnemiesOnLevelModels.Count}");
            
            if (currentEnemiesOnLevelModels.Count == 0)
            {
                Debug.Log("<color=cyan>Событие: Список пуст, вызываю EnemiesWaveFinished</color>");
                EnemiesWaveFinished.Invoke();
            }
        }
        
    }
}