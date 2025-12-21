using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Levels
{
    public class LevelsConfig
    {
        private static LevelsConfig _instance;
        public static LevelsConfig Instance => _instance ??= new LevelsConfig();

        public IReadOnlyList<LevelData> Levels => _levels;
        private List<LevelData> _levels = new();

        private LevelsConfig()
        {
            Load();
        }

        public void Load()
        {
            var json = Resources.Load<TextAsset>("LevelsConfig").text;
            var wrapper = JsonUtility.FromJson<LevelsConfigWrapper>(json);
            _levels = new List<LevelData>(wrapper.levels);
            Debug.Log($"Загружено {wrapper.levels.Length} уровней");
        }
    }

    [System.Serializable]
    public class LevelsConfigWrapper
    {
        public LevelData[] levels;
    }
}
