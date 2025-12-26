using Levels;
using Scripts.Entities;
using UnityEngine;

public class Game
{   
    public static Game Instance;
    
    // Модели систем
    private TurretSystemModel  turretSystemModel;
    private Player playerModel;
    private LevelModel levelModel;
    private BuffSystemModel buffModel;
    public LevelStats00 CurrentLevelConfig { get; set; }
    public string CurrentLevelId { get; set; }

    public TurretSystemModel TurretSystemModel
    {
        get => turretSystemModel;
        set => turretSystemModel = value;
    }

    public Player PlayerModel
    {
        get => playerModel;
        set => playerModel = value;
    }
    public LevelModel LevelModel
    {
        get => levelModel;
        set => levelModel = value;
    }

    public BuffSystemModel BuffModel
    {
        get => buffModel;
        set => buffModel = value;
    }
    
    private Game()
    {
        turretSystemModel = new TurretSystemModel();
        playerModel = new Player();
        levelModel = new LevelModel();
        
        buffModel = new BuffSystemModel(turretSystemModel, playerModel);
    }
    
    public static void Initialize()
    {
        Instance = new Game();
    }
}
