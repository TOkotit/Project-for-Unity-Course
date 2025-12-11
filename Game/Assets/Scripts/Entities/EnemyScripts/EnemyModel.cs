using Scripts.Entities;
using UnityEngine;

public class EnemyModel : Entity_Model
{
    private EnemyType enemyType;
    
    private float baseDamage;
    private float damage;
    private float fireRate;
    private float movementSpeed;

    public EnemyModel(EnemyType enemyType)
    {
        this.enemyType = enemyType;

        switch (enemyType)
        {
            case EnemyType.Car: ScriptableObject.CreateInstance<CarEnemySO>().LoadIntoModel(this);break;
            case EnemyType.Drone: ScriptableObject.CreateInstance<DroneEnemySO>().LoadIntoModel(this);break;
            default: ScriptableObject.CreateInstance<DroneEnemySO>().LoadIntoModel(this);break;
        }
    }

    public float BaseDamage
    {
        get => baseDamage;
        set => baseDamage = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }
}
