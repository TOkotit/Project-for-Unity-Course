using System.Collections.Generic;
using UnityEngine;

public class TurretSystemModel
{   
    private TurretSystemSO turretSystemSO;
    
    private List<TurretModel> turretModels;
    private int currentTurretIndex;
    private TurretModel currentTurret;
    
    public TurretSystemModel()
    {   
        turretSystemSO = Resources.Load<TurretSystemSO>("Config/TurretSystemSO");
        
        turretModels = new List<TurretModel>();
        
        foreach (var type in turretSystemSO.startTurrets) 
        {
            turretModels.Add(new TurretModel(type));
        }
        
        currentTurret = turretModels[currentTurretIndex];
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

    public void ChooseNextTurret()
    {
        currentTurretIndex = (currentTurretIndex + 1) % turretModels.Count;
        currentTurret = turretModels[currentTurretIndex];
        Debug.Log($"Текущаая турель {currentTurret.TurretType}");
    }
    public void ChoosePreviousTurret()
    {
        currentTurretIndex = (currentTurretIndex - 1 + turretModels.Count) % turretModels.Count;
        currentTurret = turretModels[currentTurretIndex];
        Debug.Log($"Текущаая турель {currentTurret.TurretType}");
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
