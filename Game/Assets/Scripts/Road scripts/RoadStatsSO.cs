using UnityEngine;

namespace Road_scripts
{
    [CreateAssetMenu(fileName = "RoadStats", menuName = "Stats/Road Stats")]
    public class RoadStatsSO : ScriptableObject
    {
        public float roadSpeed = 15f;

        [Header("Генерация")] public int visibleTilesCount = 12;

        public float recycleThresholdZ = -20f;
    }
}