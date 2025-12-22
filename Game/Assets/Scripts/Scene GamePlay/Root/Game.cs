using Levels;
using Scripts.Entities;
using UnityEngine;

public class Game
{   
    public static Game Instance;
    
    // Модели систем
    public TurretSystemModel  turretSystemModel;
    public Player playerModel;
    public LevelModel levelModel;
    public BuffSystemModel buffModel;
    
    private Game()
    {
        turretSystemModel = new TurretSystemModel();
        playerModel = new Player();
        levelModel = new LevelModel();
        
        
    }
    
    public static void Initialize()
    {
        Instance = new Game();
        
        // Последняя запускаемая система
        Instance.buffModel = new BuffSystemModel();
    }
}
