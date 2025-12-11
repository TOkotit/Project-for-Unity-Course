using Scripts.Entities;
using UnityEngine;

public class Game
{   
    public static Game Instance;
    
    // Модели систем
    public TurretSystemModel  turretSystemModel;
    public EnemySpawnerModel enemySpawnerModel;
    public Player playerModel;

    private Game()
    {
        turretSystemModel = new TurretSystemModel();
        enemySpawnerModel = new EnemySpawnerModel();
        playerModel = new Player();
    }
    
    public static void Initialize()
    {
        Instance = new Game();
    }
}
