using UnityEngine;

[CreateAssetMenu(fileName = "CarEnemySO", menuName = "EnemiesStats/CarEnemySO")]
public class CarEnemySO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float maxHealth = 50f;
    public float baseDamage = 10f;
    public float movementSpeed = 5f;
    public float fireRate = 0.5f;
    public float rotationSpeed = 5f;
    public float bulletSpeed = 30f;


    [Header("Специфические настройки")]
    public string unitName = "Default Unit";
    public Color displayColor = Color.white;

    public float GetEffectiveDamage(float bonusMultiplier)
    {
        return baseDamage * bonusMultiplier;
    }
    
    public void LoadIntoModel(EnemyModel enemyModel)
    {
        enemyModel.MaxHp = maxHealth;
        enemyModel.CurrentHp = maxHealth;
        enemyModel.BaseDamage = baseDamage;
        enemyModel.Damage = baseDamage;
        enemyModel.MovementSpeed = movementSpeed;
        enemyModel.FireRate = fireRate;
        enemyModel.BulletSpeed = bulletSpeed;
        enemyModel.RotationSpeed = rotationSpeed;
    }
}
