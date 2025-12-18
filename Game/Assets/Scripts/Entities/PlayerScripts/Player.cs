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
        // public int CurrentWeaponIndex { get; private set; } 
        //
        // public readonly UnityEvent OnWeaponSwitched = new(); 
        
        public Player() : base()
        {
            // CurrentWeaponIndex = 0;
        }

        public void Initialize(CarStatsSO carStatsSO)
        {
            carStatsSO.LoadIntoModel(this);
        }
        // public void SwitchWeapon(int newIndex)
        // {
        //     if (newIndex == CurrentWeaponIndex) return;
        //     
        //     CurrentWeaponIndex = newIndex;
        //     OnWeaponSwitched.Invoke();
        // }
    }
}