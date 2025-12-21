using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class LevelProgressManager
    {
        private static LevelProgressManager _instance;
        public static LevelProgressManager Instance => _instance ??= new LevelProgressManager();

        private Dictionary<string, LevelProgress> _progressDict = new();
        public IReadOnlyDictionary<string, LevelProgress> Progress => _progressDict;

        private const string SAVE_PATH = "progress.json";

        private LevelProgressManager()
        {
            LoadProgress();
        }

        public void LoadProgress()
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var wrapper = JsonUtility.FromJson<ProgressSaveWrapper>(json);
                _progressDict = new Dictionary<string, LevelProgress>();
                foreach (var p in wrapper.progress)
                    _progressDict[p.levelId] = p;
                Debug.Log($"Прогресс загружен: {wrapper.progress.Count} уровней");
            }
            else
            {
                Debug.Log("Файл прогресса не найден - создается default");
                InitializeDefaultProgress();
            }

            UnlockNextAvailable();
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
            string json = JsonUtility.ToJson(wrapper, true);
            string path = Path.Combine(Application.persistentDataPath, SAVE_PATH);
            File.WriteAllText(path, json);
            Debug.Log($"Прогресс сохранён: {path}");
        }

        public void CompleteLevel(string levelId, int starsEarned, float timeSeconds)
        {
            if (!_progressDict.TryGetValue(levelId, out var progress))
            {
                progress = new LevelProgress { levelId = levelId };
                _progressDict[levelId] = progress;
            }

            progress.Update(starsEarned, timeSeconds);
            SaveProgress();
            UnlockNextAvailable();
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
        }

        public bool IsLevelUnlocked(string levelId)
        {
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
