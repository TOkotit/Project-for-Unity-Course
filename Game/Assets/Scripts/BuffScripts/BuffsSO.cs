using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffsSO", menuName = "Scriptable Objects/BuffsSO")]
public class BuffsSO : ScriptableObject
{
    public List<Buff> buffs = new List<Buff>()
    {
        new Buff("Урон турелей", ParameterType.TurretsDamage, 0.1f, 0, 10),
        new Buff("Скорострельность турелей", ParameterType.TurretsFireRate, 0.1f, 0, 10),
        new Buff("Количество турелей", ParameterType.TurretSlots, 1, 0, 10),
        
        new Buff("Здоровье игрока", ParameterType.PlayerHp, 0.1f, 0, 10),
    };
}