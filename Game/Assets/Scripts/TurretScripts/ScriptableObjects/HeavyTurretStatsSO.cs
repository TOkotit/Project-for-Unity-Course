using UnityEngine;

[CreateAssetMenu(fileName = "HeavyTurretStatsSO", menuName = "TurrerStats/HeavyTurretStatsSO")]
public class HeavyTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 1f;
    public float damage = 50f;
    public float range = 800f;
    public float bulletSpeed = 50f;

    public void LoadIntoModel(TurretModel turretModel)
    {
        turretModel.BulletSpeed = bulletSpeed;
        turretModel.Damage = damage;
        turretModel.Range = range;
        turretModel.RotationSpeed = rotationSpeed;
        turretModel.FireRate = fireRate;
    }
}
