using Levels;
using Scripts.Entities;
using UnityEngine;

public class Game
{   
    public static Game Instance;
    
    // Модели систем
    public TurretSystemModel  turretSystemModel;
    public EnemySpawnerModel enemySpawnerModel;
    public Player playerModel;
    public LevelModel levelModel;
    public string CurrentLevelId { get; set; }

    private Game()
    {
        turretSystemModel = new TurretSystemModel();
        enemySpawnerModel = new EnemySpawnerModel();
        playerModel = new Player();
        levelModel = new LevelModel();
    }
    
    public static void Initialize()
    {
        Instance = new Game();
    }
}
