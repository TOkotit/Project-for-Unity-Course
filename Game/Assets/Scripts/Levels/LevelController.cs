using System;
using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {

        private LevelModel levelModel;

        public void Awake()
        {
            levelModel.EnemiesWaveFinished.AddListener(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            // логика того, как создаёт врагов, их классов пока нет
            
        }
    }
}