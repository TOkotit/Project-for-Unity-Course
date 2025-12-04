using UnityEngine;

[CreateAssetMenu(fileName = "LightTurretStatsSO", menuName = "Scriptable Objects/LightTurretStatsSO")]
public class LightTurretStatsSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public float rotationSpeed = 10f;
    public float fireRate = 4f;
    public float damage = 10;
    public float range = 800f;
    public float bulletSpeed = 90f;
    
    [Header("Специфические настройки")]
    public string unitName = "HeavyTurret Unit";
    public Color displayColor = Color.greenYellow;
}
