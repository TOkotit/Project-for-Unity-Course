using UnityEngine;

[CreateAssetMenu(fileName = "MediumTurretStatsSO", menuName = "Stats/MediumTurretStatsSO")]
public class MediumTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 2f;
    public float damage = 25f;
    public float range = 800f;
    public float bulletSpeed = 80f;
    
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
