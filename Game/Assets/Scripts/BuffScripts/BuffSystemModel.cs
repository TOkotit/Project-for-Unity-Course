using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Scripts.Entities;
using UnityEngine.Events;

public class BuffSystemModel
{
    private TurretSystemModel turretSystemModel;
    private Player playerModel;

    private string savePath;
    public List<Buff> Buffs { get; private set; }
    
    private int points;

    private bool hasBuffsSave;
    public readonly UnityEvent PointsChanged = new();

    public int Points => points;

    public bool HasBuffsSave
    {
        get => hasBuffsSave;
        set => hasBuffsSave = value;
    }

    public BuffSystemModel(TurretSystemModel turretSystem, Player player)
    {
        turretSystemModel = turretSystem;
        playerModel = player;
        
        savePath = Path.Combine(Application.persistentDataPath, "buffs_save.json");
        
        Buffs = new List<Buff>();

        hasBuffsSave = LoadBuffs();
        
        if (!hasBuffsSave)
        {
            InitializeFromSO(Resources.Load<BuffsSO>("Config/BuffsSO"));
        }
    }

    private void InitializeFromSO(BuffsSO so)
    {
        Buffs.Clear();
        foreach (var buff in so.buffs)
        {
            Buffs.Add(new Buff(buff.Name, buff.ParameterType, buff.Value, buff.BuffLevel, buff.MaxBuffLevel, buff.Cost));
        }
        points = so.Points;
        Debug.Log("Усиления загружены через SO");
    }

    public void ApplyAllBuffs()
    {
        turretSystemModel.ResetTurrets();
        foreach (var buff in Buffs)
        {
            switch (buff.ParameterType)
            {
                case ParameterType.TurretsDamage:
                    turretSystemModel.ApplyDamageBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied Damage Buff: Level {buff.BuffLevel}");
                    break;
                case ParameterType.TurretsFireRate:
                    turretSystemModel.ApplyFireRateBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied FireRate Buff: Level {buff.BuffLevel}");
                    break;
                case ParameterType.PlayerHp:
                    playerModel.ApplyHpBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied PlayerHP Buff: Level {buff.BuffLevel}");
                    break;
            }
        }
    }

    public void ClearBuffsSave()
    {
        InitializeFromSO(Resources.Load<BuffsSO>("Config/BuffsSO"));
        var path = Path.Combine(Application.persistentDataPath, savePath);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        Debug.Log("Прогресс усилений сброшен");
    }
    
    public void LevelUpBuff(int index)
    {
        if (index >= 0 && index < Buffs.Count)
        {
            if (Buffs[index].BuffLevel < Buffs[index].MaxBuffLevel)
            {
                if (points >= Buffs[index].Cost)
                {
                    Buffs[index].BuffLevel++;
                    points -= Buffs[index].Cost;
                    PointsChanged.Invoke();
                    SaveBuffs();
                }
                else
                {
                    Debug.Log("Недостаточно очков");
                }

            }
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        PointsChanged.Invoke();
        SaveBuffs();
        Debug.Log($"Получено очков {amount}, всего {points}");
    }
    
    public void SaveBuffs()
    {
        BuffListWrapper wrapper = new BuffListWrapper { list = Buffs, points = points };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Buffs saved to: " + savePath);
    }
    
    public bool LoadBuffs()
    {
        if (!File.Exists(savePath)) return false;
    
        try
        {
            string json = File.ReadAllText(savePath);
            BuffListWrapper wrapper = JsonUtility.FromJson<BuffListWrapper>(json);
            
            if (wrapper != null && wrapper.list != null)
            {
                Buffs = wrapper.list;
                points = wrapper.points;
                Debug.Log("Buffs loaded successfully.");
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load buffs: " + e.Message);
        }
        return false;
    }
    
    // Вспомогательный класс-обертка для JSON
    [System.Serializable]
    private class BuffListWrapper
    {
        public List<Buff> list;
        public int points;
    }
}