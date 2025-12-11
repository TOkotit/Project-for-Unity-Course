using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour
{   
    private List<Turret> turrets;
    
    private TurretSystemModel turretSystemModel;

    public void Awake()
    {
        turretSystemModel = Game.Instance.turretSystemModel;
    }
}
