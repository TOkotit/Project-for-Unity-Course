using System.Collections.Generic;
using UnityEngine;

public class TurretSystemModel
{   
    private TurretSystemSO turretSystemSO;
    
    private List<TurretModel> turretModels;
    private TurretModel currentTurret;
    
    public TurretSystemModel()
    {   
        turretSystemSO = Resources.Load<TurretSystemSO>("Config/TurretSystemSO");
        
        turretModels = new List<TurretModel>();
        
        foreach (var type in turretSystemSO.startTurrets) 
        {
            turretModels.Add(new TurretModel(type));
        }
    }
    public List<TurretModel> TurretModels
    {
        get => turretModels;
        set => turretModels = value;
    }

    public TurretModel CurrentTurret
    {
        get => currentTurret;
        set => currentTurret = value;
    }

    public void ChooseTurret(TurretModel turret)
    {
        currentTurret = turret;
    }

    public void CancelChoosingTurret()
    {
        currentTurret = null;
    }

    public void ActivateChosenTurret(Transform target)
    {
        if (currentTurret is not null)
        {
            currentTurret.CurrentTarget = target;
        }
    }
}
