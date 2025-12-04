using UnityEngine;

[CreateAssetMenu(fileName = "MediumTurretStatsSO", menuName = "Scriptable Objects/MediumTurretStatsSO")]
public class MediumTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 2f;
    public float damage = 25f;
    public float range = 800f;
    public float bulletSpeed = 70f;
    
    [Header("Специфические настройки")]
    public string unitName = "HeavyTurret Unit";
    public Color displayColor = Color.coral;
}
