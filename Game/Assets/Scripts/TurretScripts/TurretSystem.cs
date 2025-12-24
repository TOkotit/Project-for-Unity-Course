using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSystem : MonoBehaviour
{   
    [SerializeField] Turret heavyTurretPrefab;
    [SerializeField] Turret mediumTurretPrefab;
    [SerializeField] Turret lightTurretPrefab;
    
    public List<Turret> turrets;
    public TurretSystemModel turretSystemModel;

    public void Awake()
    {
        turretSystemModel = Game.Instance.TurretSystemModel;
        turretSystemModel.Initialize();
        var turretPosition = new Vector3(0, 0, 0);
        
        foreach (var elem in turretSystemModel.TurretModels)
        {
            switch (elem.TurretType)
            {
                case TurretType.Heavy: CreateTurret(heavyTurretPrefab, elem, turretPosition); break;
                case TurretType.Medium: CreateTurret(mediumTurretPrefab, elem, turretPosition); break;
                case TurretType.Light: CreateTurret(lightTurretPrefab, elem, turretPosition); break;
            }
            turretPosition += new Vector3(0, 0, -1);
        }
    }

    private void CreateTurret(Turret prefab, TurretModel model, Vector3 turretPosition)
    {
        var newTurret = Instantiate(prefab, transform.position, Quaternion.identity)
            .GetComponent<Turret>();
        
        newTurret.transform.SetParent(this.transform);
        
        newTurret.transform.localPosition = turretPosition;
        newTurret.transform.localRotation = Quaternion.identity;

        newTurret.turretModel = model;
        turrets.Add(newTurret);
    }

    public void ChoosePrevious(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            turretSystemModel.ChoosePreviousTurret();
        }
    }
    public void ChooseNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            turretSystemModel.ChooseNextTurret();
        }
    }
}
