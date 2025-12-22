using System;
using UnityEngine;

[Serializable]
public class Buff
{
    public string Name;          
    public ParameterType Type; 
    public float Value;         
    public int BuffLevel;
    public int MaxBuffLevel;
    
    public Buff() { }

    public Buff(string name, ParameterType type, float value, int buffLevel, int maxBuffLevel)
    {
        Name = name;
        Type = type;
        Value = value;
        BuffLevel = buffLevel;
        MaxBuffLevel = maxBuffLevel;
    }
}
