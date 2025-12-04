using UnityEngine;

namespace Truck_Sripts
{
    [CreateAssetMenu(fileName = "NewCarStats", menuName = "Stats/Car Stats")]
    public class CarStatsSO : ScriptableObject
    {
        [Header("Базовые характеристики")]
        public float SideSpeed = 10f;
        public float maxSidePosition = 5f;
        public float maxSteerAngle = 15f;
        public float rotationSpeed = 5f;
        public float snapBackSpeed = 20f;
        
        [Header("Физические ограничения")]
        public float fixedYPosition = 0f; 
        
        public RigidbodyConstraints constraints = 
            RigidbodyConstraints.FreezePositionZ | 
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotationX | 
            RigidbodyConstraints.FreezeRotationZ;
    }
}