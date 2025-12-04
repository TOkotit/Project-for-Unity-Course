using System;
using Entities;
using UnityEngine;
using UnityEngine.Events;


namespace Scripts.Entities
{
    
    [System.Serializable]
    public class HealthParamEvent : UnityEvent<float, float> { }
    
    public class Entity_Model
    {
        private float _currentHp;
        public readonly HealthParamEvent CurrentHpChanged = new();

        private float _maxHp;
        public readonly HealthParamEvent MaxHpChanged = new();

        public readonly UnityEvent ImJustDie = new();
        
        
        
        public float MaxHp => _maxHp;
        public float CurrentHp => _currentHp;

        public Entity_Model()
        {
            var settings = Resources.Load<UnitStatsSO>("Config/BaseUnitStats");
            
            if (!settings)
            {
                Debug.LogError("Критическая ошибка: UnitStatsSO не найден в Resources/Config!");
                return;
            }

            _maxHp = settings.maxHealth;
            _currentHp = _maxHp;
        }
        
        public void TakeDamage(float damage)
        {
            if (_currentHp <= 0) return; 

            _currentHp -= damage;
            
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp); 
            
            CurrentHpChanged.Invoke(_currentHp, _maxHp);
            
            if (_currentHp <= 0)
                ImJustDie.Invoke();
        }
    }
}