using System.Collections.Generic;
using UnityEngine;

public class TurretSystemModel
{
    private List<TurretModel> turrets;
    private TurretModel currentTurret;

    public TurretSystemModel()
    {
        turrets = new List<TurretModel>();
    }
    public List<TurretModel> Turrets
    {
        get => turrets;
        set => turrets = value;
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
