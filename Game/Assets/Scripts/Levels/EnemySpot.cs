using UnityEngine;

namespace Levels
{
    public class EnemySpot
    {
        public Vector3 Position;
        public bool IsFree;

        public EnemySpot(Vector3 position)
        {
            Position = position;
            IsFree = true;
        }
    }
}