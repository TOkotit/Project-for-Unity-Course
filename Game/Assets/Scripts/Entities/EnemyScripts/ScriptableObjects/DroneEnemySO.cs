using UnityEngine;

[CreateAssetMenu(fileName = "DroneEnemySO", menuName = "EnemiesStats/DroneEnemySO")]
public class DroneEnemySO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float maxHealth = 100f;
    public float baseDamage = 30f;
    public float movementSpeed = 10f;
    public float fireRate = 0.5f;
    public float rotationSpeed = 10f;
    public float bulletSpeed = 30f;
    public int rewardPoints = 1;
    public float fireCountdown = 4f;



    
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
        enemyModel.RewardPoints = rewardPoints;
        enemyModel.FireCountdown = fireCountdown;

    }
}
