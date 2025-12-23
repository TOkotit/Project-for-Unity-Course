using System;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    [System.Serializable]
    public class LevelProgress
    {
        public string levelId;
        public bool isCompleted;     
        public float bestTime;       
        public int attempts;
        public long lastPlayedTicks;

        public void MarkAsCompleted(float time)
        {
            isCompleted = true;
            bestTime = bestTime == 0 ? time : Mathf.Min(bestTime, time);
            attempts++;
            lastPlayedTicks = DateTime.UtcNow.Ticks;
        }
    }
}
