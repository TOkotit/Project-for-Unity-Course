using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretSystemSO", menuName = "TurrerStats/TurretSystemSO")]
public class TurretSystemSO : ScriptableObject
{
    [Header("Базовые характеристики")]
    public List<TurretType> startTurrets = new()
    {
        TurretType.Heavy, 
        TurretType.Medium, 
        TurretType.Light
    };
}
