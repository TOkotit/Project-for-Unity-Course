using Scripts.Entities;
using UnityEngine;

public class EnemyModel : Entity_Model
{ 
    public EnemyType EnemyType { get; private set; }
    private float baseDamage;
    private float damage;
    private float fireRate;
    private float movementSpeed;


    public EnemyModel(EnemyType enemyType)
    {
        EnemyType = enemyType;
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
    
    public void LoadStatsFromSO(CarEnemySO stats)
    { 
        stats.LoadIntoModel(this);
    }

    public void LoadStatsFromSO(DroneEnemySO stats)
    {
        stats.LoadIntoModel(this);
    }
}
