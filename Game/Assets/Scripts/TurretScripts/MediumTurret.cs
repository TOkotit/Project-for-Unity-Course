using UnityEngine;

public class MediumTurret : Turret
{
    [SerializeField] private MediumTurretStatsSO mediumTurretStats;

    public new void Awake()
    {
        base.Awake();
        
        turretModel.TurretType = TurretType.Medium;

        turretModel.RotationSpeed = mediumTurretStats.rotationSpeed;
        turretModel.BulletSpeed = mediumTurretStats.bulletSpeed;
        turretModel.Range = mediumTurretStats.range;
        turretModel.Damage = mediumTurretStats.damage;
        turretModel.FireRate = mediumTurretStats.fireRate;
    }
}
