using UnityEngine;

namespace Road_scripts
{
    [CreateAssetMenu(fileName = "RoadStats", menuName = "Stats/Road Stats")]
    public class RoadStatsSO : ScriptableObject
    {
        public float roadSpeed = 15f;

        [Header("Генерация")] public int visibleTilesCount = 12;

        public float recycleThresholdZ = -45f;

        public void LoadIntoModel(RoadGenerator roadGenerator)
        {
            roadGenerator.RoadSpeed =  roadSpeed;
            roadGenerator.VisibleTilesCount = visibleTilesCount; 
            roadGenerator.RecycleThresholdZ = recycleThresholdZ;
            
        }
    }
}