using System.Collections.Generic;
using Road_scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Levels
{
    [CreateAssetMenu(fileName = "Level1Stats", menuName = "LevelStats/Level1StatsSO")]

    public class LevelStats00 : ScriptableObject
    {
        public List<EnemyModel> EnemyPoolConfiguration = new()
        {
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Car),
            new EnemyModel(EnemyType.Drone)
        };
        public List<int> WavesConfiguration = new() { 3, 2, 2 };

        [SerializeField] public List<RoadTile> RoadTiles;
        
        
        public List<EnemyModel> GetNextSpawnEntry()
        {
            if (EnemyPoolConfiguration is null || EnemyPoolConfiguration.Count == 0)
            {
                return null;
            }

            var outPutList = new List<EnemyModel>();
            for (var i = WavesConfiguration[0] - 1; i >= 0; i--)
            {
                outPutList.Add(EnemyPoolConfiguration[i]);
                EnemyPoolConfiguration.RemoveAt(i);
                
                
            }
            WavesConfiguration.RemoveAt(0);
            
            
            return outPutList;
        }

        public void LoadIntoModel(LevelModel levelModel)
        {
            levelModel.EnemyPoolConfiguration = EnemyPoolConfiguration;
            levelModel.WavesConfiguration = WavesConfiguration;
        }
    }
}