using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour
{   
    [SerializeField] private List<Turret> turrets;
    public TurretSystemModel turretSystemModel;
    
    

    public TurretSystem()
    {
        turretSystemModel = new TurretSystemModel();
    }

    public void Awake()
    {
        //var c = GetComponentsInChildren<Turret>();
        
        foreach (var t in turrets)
        {
            turretSystemModel.Turrets.Add(t.turretModel);
        }
        Debug.Log($"Кол-во турелей{turretSystemModel.Turrets.Count}");
    }
}
