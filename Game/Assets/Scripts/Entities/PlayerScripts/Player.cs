using System;
using System.Collections.Generic;
using System.Linq;
using Truck_Sripts;
using UnityEngine;
using UnityEngine.Events;


namespace Scripts.Entities
{
    public class Player : Entity_Model
    {
        public Player() : base()
        {
            
        }

        public void Initialize(CarStatsSO carStatsSO)
        {
            carStatsSO.LoadIntoModel(this);
        }


        public void ApplyHpBuff(float buff)
        {
            MaxHp *= 1 + buff;
        }
    }
}