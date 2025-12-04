using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "NewUnitStats", menuName = "Stats/Unit Stats")]
    public class UnitStatsSO : ScriptableObject
    {
        [Header("Базовые характеристики")]
        public float maxHealth = 100f;
        public float baseDamage = 10f;
        public float movementSpeed = 5f;

        [Header("Специфические настройки")]
        public string unitName = "Default Unit";
        public Color displayColor = Color.white;

        public float GetEffectiveDamage(float bonusMultiplier)
        {
            return baseDamage * bonusMultiplier;
        }
    }
}