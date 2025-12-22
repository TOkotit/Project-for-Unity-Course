using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Scripts.Entities;

public class BuffSystemModel
{
    public List<Buff> Buffs { get; private set; }
    
    private TurretSystemModel turretSystemModel;
    private Player playerModel;

    private string savePath;

    public BuffSystemModel()
    {
        turretSystemModel = Game.Instance.turretSystemModel;
        playerModel = Game.Instance.playerModel;
        
        savePath = Path.Combine(Application.persistentDataPath, "buffs_save.json");
        
        Buffs = new List<Buff>();
        
        // Пробуем загрузить, если файла нет - берем из SO
        if (!LoadBuffs())
        {
            var defaultBuffsData = Resources.Load<BuffsSO>("Config/BuffsSO");
            InitializeFromSO(defaultBuffsData);
        }
    }

    private void InitializeFromSO(BuffsSO so)
    {
        Buffs.Clear();
        foreach (var buff in so.buffs)
        {
            Buffs.Add(new Buff(buff.Name, buff.Type, buff.Value, buff.BuffLevel, buff.MaxBuffLevel));
        }
    }

    public void ApplyAllBuffs()
    {
        foreach (var buff in Buffs)
        {
            switch (buff.Type)
            {
                case ParameterType.TurretsDamage:
                    turretSystemModel.ApplyDamageBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied Damage Buff: Level {buff.BuffLevel}");
                    break;
                case ParameterType.TurretsFireRate:
                    turretSystemModel.ApplyFireRateBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied FireRate Buff: Level {buff.BuffLevel}");
                    break;
                case ParameterType.TurretSlots:
                    turretSystemModel.MaxTurretSlots = buff.BuffLevel;
                    Debug.Log($"Applied Slots Buff: Level {buff.BuffLevel}");
                    break;
                case ParameterType.PlayerHp:
                    playerModel.ApplyHpBuff(buff.Value * buff.BuffLevel);
                    Debug.Log($"Applied PlayerHP Buff: Level {buff.BuffLevel}");
                    break;
            }
        }
    }

    public void LevelUpBuff(int index)
    {
        if (index >= 0 && index < Buffs.Count)
        {
            if (Buffs[index].BuffLevel < Buffs[index].MaxBuffLevel)
            {
                Buffs[index].BuffLevel++;
                SaveBuffs();
            }
        }
    }

    // JSON
    public void SaveBuffs()
    {
        BuffListWrapper wrapper = new BuffListWrapper { list = Buffs };
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
    }
}