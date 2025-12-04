using UnityEngine;

public class LightTurret : Turret
{
    [SerializeField] private LightTurretStatsSO lightTurretStats;

    public new void Awake()
    {
        base.Awake();
        
        turretModel.TurretType = TurretType.Light;

        turretModel.RotationSpeed = lightTurretStats.rotationSpeed;
        turretModel.BulletSpeed = lightTurretStats.bulletSpeed;
        turretModel.Range = lightTurretStats.range;
        turretModel.Damage = lightTurretStats.damage;
        turretModel.FireRate = lightTurretStats.fireRate;
    }
}
