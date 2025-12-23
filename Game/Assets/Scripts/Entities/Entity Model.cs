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

        public readonly UnityEvent OnDeath = new();

        public Entity_Model()
        {
            
        }
        
        public float MaxHp {
            get => _maxHp;
            set => _maxHp = value;
        }

        public float CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public void SetupHealth(float health)
        {
            MaxHp = health;
            CurrentHp = health;
        }
        
        public virtual void TakeDamage(float amount)
        {
            if (CurrentHp <= 0) return;
            CurrentHp = Mathf.Clamp(CurrentHp - amount, 0, MaxHp);
            CurrentHpChanged.Invoke(CurrentHp, MaxHp);
            if (CurrentHp <= 0) OnDeath.Invoke();
        }
    }
}