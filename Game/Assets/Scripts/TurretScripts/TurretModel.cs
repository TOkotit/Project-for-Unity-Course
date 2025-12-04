using Unity.VisualScripting;
using UnityEngine;

public class TurretModel
{
    private float rotationSpeed;
    private float fireRate;
    private float damage;
    private float range;
    private float bulletSpeed;
    private float fireCountdown;
    private TurretType turretType;
    
    private Transform currentTarget;
    
    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }
    public float Damage 
    {
        get => damage;
        set => damage = value;
    }
    public float Range 
    {
        get => range;
        set => range = value;
    }
    public float FireCountdown
    {
        get => fireCountdown;
        set => fireCountdown = value;
    }
    public TurretType TurretType
    {
        get => turretType;
        set => turretType = value;
    }
    public float BulletSpeed
    {
        get => bulletSpeed;
        set  => bulletSpeed = value;
    }
    public Transform  CurrentTarget
    {
        get => currentTarget;
        set => currentTarget = value;
    }
}
