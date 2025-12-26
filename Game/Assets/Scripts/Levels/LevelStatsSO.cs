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
        public List<int> WavesConfiguration;

        [SerializeField] public List<RoadTile> RoadTiles;
        
        public void LoadIntoModel(LevelModel levelModel)
        {
            levelModel.WavesConfiguration = new List<int>(WavesConfiguration);
        }
    }
}