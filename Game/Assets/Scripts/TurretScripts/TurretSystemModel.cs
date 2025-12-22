using System.Collections.Generic;
using UnityEngine;

public class TurretSystemModel
{   
    private TurretSystemSO turretSystemSO;
    
    private int maxTurretSlots;
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
        maxTurretSlots = turretModels.Count;
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

    public int MaxTurretSlots
    {
        get => maxTurretSlots;
        set => maxTurretSlots = value;
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

    public void ActivateChosenTurret(GameObject target)
    {
        if (currentTurret is not null)
        {
            currentTurret.CurrentTarget = target.transform;
            currentTurret.CurrentEnemy = target.GetComponent<Enemy>().Model;
        }
    }

    public void ApplyDamageBuff(float buff)
    {
        foreach (var turretModel in turretModels)
        {
            turretModel.Damage *= 1 + buff;
        }
    }
    
    public void ApplyFireRateBuff(float buff)
    {
        foreach (var turretModel in turretModels)
        {
            turretModel.FireRate *= 1 + buff;
        }
    }
}
