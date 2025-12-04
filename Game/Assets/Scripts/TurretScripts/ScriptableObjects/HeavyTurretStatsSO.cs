using UnityEngine;

[CreateAssetMenu(fileName = "HeavyTurretStatsSO", menuName = "Stats/HeavyTurretStatsSO")]
public class HeavyTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 1f;
    public float damage = 50f;
    public float range = 800f;
    public float bulletSpeed = 50f;
    
    [Header("Специфические настройки")]
    public string unitName = "Turret Unit";
    public Color displayColor = Color.blue;

    public void LoadIntoModel(TurretModel turretModel)
    {
        turretModel.BulletSpeed = bulletSpeed;
        turretModel.Damage = damage;
        turretModel.Range = range;
        turretModel.RotationSpeed = rotationSpeed;
        turretModel.FireRate = fireRate;
    }
}
