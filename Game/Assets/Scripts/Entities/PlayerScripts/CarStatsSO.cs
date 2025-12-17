using Entities.PlayerScripts;
using Scripts.Entities;
using UnityEngine;

namespace Truck_Sripts
{
    [CreateAssetMenu(fileName = "NewCarStats", menuName = "PlayerStats/CarStatsSO")]
    public class CarStatsSO : ScriptableObject
    {
        [Header("Базовые характеристики")] public float
            maxHP = 100f;
        
        [Header("Физические ограничения")]
        public float fixedYPosition = 0f; 
        public float SideSpeed = 10f;
        public float maxSidePosition = 5f;
        public float maxSteerAngle = 15f;
        public float rotationSpeed = 5f;
        public float snapBackSpeed = 20f;
        
        public RigidbodyConstraints constraints = 
            RigidbodyConstraints.FreezePositionZ | 
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotationX | 
            RigidbodyConstraints.FreezeRotationZ;
        
        public void LoadIntoController(PlayerController player)
        {
            player.SideSpeed = SideSpeed;
            player.maxSidePosition = maxSidePosition;
            player.maxSteerAngle = maxSteerAngle;
            player.rotationSpeed = rotationSpeed;
            player.snapBackSpeed = snapBackSpeed;
            player.constraints = constraints;
            player.fixedYPosition = fixedYPosition;
        }

        public void LoadIntoModel(Player player)
        {
            player.MaxHp =  maxHP;
            player.CurrentHp = maxHP;
        }
    }
}