using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class LevelProgressManager
    {
        private static LevelProgressManager _instance;
        private Dictionary<string, LevelProgress> _progressDict = new();
        private const string SAVE_PATH = "progress.json";
        public static LevelProgressManager Instance => _instance ??= new LevelProgressManager();
        public IReadOnlyDictionary<string, LevelProgress> Progress => _progressDict;

        private LevelProgressManager()
        {
            LoadProgress();
        }

        public void LoadProgress()
        {
            var path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var wrapper = JsonUtility.FromJson<ProgressSaveWrapper>(json);
                _progressDict = new Dictionary<string, LevelProgress>();
                foreach (var p in wrapper.progress)
                    _progressDict[p.levelId] = p;
                Debug.Log($"Прогресс загружен: пройдено {wrapper.progress.Count} уровней");
            }
            else
            {
                _progressDict = new Dictionary<string, LevelProgress>();
                return;
            }

            Debug.Log("Текущее содержимое _progressDict после LoadProgress():");
            foreach (var kv in _progressDict)
                Debug.Log($" - {kv.Key} : isCompleted={kv.Value.isCompleted}, bestTime={kv.Value.bestTime}, attempts={kv.Value.attempts}");
            
            
            UnlockNextAvailable();
        }
        
        public bool HasAnyProgress()
        {
            foreach (var p in _progressDict.Values)
            {
                if (p.isCompleted) return true;
            }
            return false;
                
        }
        
        public void ClearAllProgress()
        {
            _progressDict.Clear();
    
            var path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            if (File.Exists(path)) File.Delete(path);

            InitializeDefaultProgress(); 
            UnlockNextAvailable();
            Debug.Log("Сохранения удалены. Начата новая игра.");
        }
        
        public void ClearProgress()
        {
            _progressDict.Clear();
    
            var path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            InitializeDefaultProgress();
            UnlockNextAvailable();
            Debug.Log("Прогресс полностью сброшен (Новая игра)");
        }
        
        private void InitializeDefaultProgress()
        {
            _progressDict.Clear();
            var first = LevelsConfig.Instance.Levels[0];
            _progressDict[first.id] = new LevelProgress { levelId = first.id };
        }

        public void SaveProgress()
        {
            var wrapper = new ProgressSaveWrapper
            {
                progress = new List<LevelProgress>(_progressDict.Values)
            };
            var json = JsonUtility.ToJson(wrapper, true);
            Debug.Log($"Saving progress JSON:\n{json}");

            var path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            File.WriteAllText(path, json);
            Debug.Log($"Прогресс сохранён в {path}");
        }

        public void CompleteLevel(string levelId, float timeSeconds)
        {
            Debug.Log($"CompleteLevel called for '{levelId}'");

            var levelExistsInConfig = LevelsConfig.Instance.Levels.Any(l => l.id == levelId);
            if (!levelExistsInConfig) Debug.LogWarning($"CompleteLevel: уровень {levelId} не найден в LevelsConfig!");
            if (!_progressDict.TryGetValue(levelId, out var progress))
            {
                progress = new LevelProgress { levelId = levelId };
                _progressDict[levelId] = progress;
            }

            progress.MarkAsCompleted(timeSeconds);
            UnlockNextAvailable();
            SaveProgress();
        }

        private void UnlockNextAvailable()
        {
            var levels = LevelsConfig.Instance.Levels;
            for (int i = 1; i < levels.Count; i++)
            {
                var prev = levels[i - 1];
                var current = levels[i];

                if (_progressDict.TryGetValue(prev.id, out var prevProgress) &&
                    prevProgress.isCompleted)
                {
                    if (!_progressDict.ContainsKey(current.id))
                        _progressDict[current.id] = new LevelProgress { levelId = current.id };
                }
                else
                {
                    break;
                }
            }
            
            Debug.Log("Состояние _progressDict после UnlockNextAvailable():");
            for (int i = 0; i < levels.Count; i++)
            {
                var id = levels[i].id;
                var inDict = _progressDict.TryGetValue(id, out var p);
                Debug.Log($" Level {i+1} ({id}) -> inDict={inDict}, isCompleted={(p != null ? p.isCompleted.ToString() : "N/A")}");
            }
        }

        public bool IsLevelUnlocked(string levelId)
        {
            if (LevelsConfig.Instance.Levels.Count > 0 && levelId == LevelsConfig.Instance.Levels[0].id)
                return true;
            return _progressDict.ContainsKey(levelId);
        }

        public LevelProgress GetProgress(string levelId)
        {
            return _progressDict.TryGetValue(levelId, out var p) ? p : null;
        }
    }

    [System.Serializable]
    public class ProgressSaveWrapper
    {
        public List<LevelProgress> progress;
    }
}
