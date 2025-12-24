using Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;

public class EnemyModel : Entity_Model
{ 
    public EnemyType EnemyType { get; private set; }
    private float baseDamage;
    private float damage;
    private float fireRate;
    private float movementSpeed;
    private float rotationSpeed;
    private float bulletSpeed;
    private float fireCountdown;
    private int rewardPoints;

    private BuffSystemModel buffSystemModel;
    
    public EnemyModel(EnemyType enemyType)
    {
        EnemyType = enemyType;
        CurrentHpChanged.Invoke(CurrentHp, MaxHp);
    }

    public void InitializePointsReward(BuffSystemModel buffSystem)
    {
        buffSystemModel = buffSystem;
        OnDeath.AddListener(GivePoints);
    }
    private void GivePoints()
    {
        buffSystemModel.AddPoints(rewardPoints); 
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

    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    public float FireCountdown
    {
        get => fireCountdown;
        set => fireCountdown = value;
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public int RewardPoints
    {
        get => rewardPoints;
        set => rewardPoints = value;
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
