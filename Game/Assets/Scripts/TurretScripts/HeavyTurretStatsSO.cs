using UnityEngine;

[CreateAssetMenu(fileName = "HeavyTurretStatsSO", menuName = "Stats/HeavyTurretStatsSO")]
public class HeavyTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public float damage = 50f;
    public float range = 800f;
    public float bulletSpeed = 50f;
    
    [Header("Специфические настройки")]
    public string unitName = "HeavyTurret Unit";
    public Color displayColor = Color.red;
}
