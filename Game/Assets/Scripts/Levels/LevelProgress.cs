using System;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    [System.Serializable]
    public class LevelProgress
    {
        public string levelId;
        public bool isCompleted;
        public int starsEarned;      
        public float bestTime;       
        public int attempts;
        public long lastPlayedTicks;

        public void Update(int stars, float time)
        {
            isCompleted = true;
            starsEarned = Mathf.Max(starsEarned, stars);
            bestTime = bestTime == 0 ? time : Mathf.Min(bestTime, time);
            attempts++;
            lastPlayedTicks = DateTime.UtcNow.Ticks;
        }
    }
}
