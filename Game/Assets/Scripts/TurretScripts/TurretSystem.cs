using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour
{   
    //TODO позже заменить на что-то типо словаря
    [SerializeField] Turret heavyTurretPrefab;
    [SerializeField] Turret mediumTurretPrefab;
    [SerializeField] Turret lightTurretPrefab;
    
    public List<Turret> turrets;
    private TurretSystemModel turretSystemModel;

    public void Awake()
    {
        turretSystemModel = Game.Instance.turretSystemModel;

        foreach (var elem in turretSystemModel.TurretModels)
        {
            switch (elem.TurretType)
            {
                case TurretType.Heavy: CreateTurret(heavyTurretPrefab, elem); break;
                case TurretType.Medium: CreateTurret(mediumTurretPrefab, elem); break;
                case TurretType.Light: CreateTurret(lightTurretPrefab, elem); break;
            }
        }
    }

    private void CreateTurret(Turret prefab, TurretModel model)
    {
        var newTurret = Instantiate(prefab, gameObject.transform.position, Quaternion.identity)
            .GetComponent<Turret>();
        
        newTurret.transform.SetParent(this.transform);
        
        newTurret.transform.localPosition = Vector3.zero;
        newTurret.transform.localRotation = Quaternion.identity;

        newTurret.turretModel = model;
        turrets.Add(newTurret);
    }
}
