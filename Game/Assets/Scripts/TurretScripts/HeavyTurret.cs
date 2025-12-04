using UnityEngine;

public class HeavyTurret : Turret
{
    [SerializeField] private HeavyTurretStatsSO heavyTurretStats;

    public new void Awake()
    {
        base.Awake();
        
        turretModel.TurretType = TurretType.Heavy;

        turretModel.RotationSpeed = heavyTurretStats.rotationSpeed;
        turretModel.BulletSpeed = heavyTurretStats.bulletSpeed;
        turretModel.Range = heavyTurretStats.range;
        turretModel.Damage = heavyTurretStats.damage;
        turretModel.FireRate = heavyTurretStats.fireRate;
    }
}
