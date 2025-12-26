using UnityEngine;

[CreateAssetMenu(fileName = "LightTurretStatsSO", menuName = "TurrerStats/LightTurretStatsSO")]
public class LightTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 41f;
    public float damage = 10f;
    public float range = 800f;
    public float bulletSpeed = 80f;

    
    public void LoadIntoModel(TurretModel turretModel)
    {
        turretModel.BulletSpeed = bulletSpeed;
        turretModel.Damage = damage;
        turretModel.Range = range;
        turretModel.RotationSpeed = rotationSpeed;
        turretModel.FireRate = fireRate;
    }
}
