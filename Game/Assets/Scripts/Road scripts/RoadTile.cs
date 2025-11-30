using UnityEngine;

namespace Road_scripts
{
    public class RoadTile : MonoBehaviour
    {
        public void Move(float speed)
        {
            transform.Translate(Vector3.back * (speed * Time.deltaTime));
        }
    }
}