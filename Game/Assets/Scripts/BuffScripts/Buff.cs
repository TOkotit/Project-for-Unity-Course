using System;
using UnityEngine;

[Serializable]
public class Buff
{
    [SerializeField] private string name; 
    [SerializeField] private ParameterType parameterType;
    [SerializeField] private float value;
    [SerializeField] private int buffLevel;
    [SerializeField] private int maxBuffLevel;

    public Buff() { }

    public Buff(string name, ParameterType parameterType, float value, int buffLevel, int maxBuffLevel)
    {
        this.name = name;
        this.parameterType = parameterType;
        this.value = value;
        this.buffLevel = buffLevel;
        this.maxBuffLevel = maxBuffLevel;
    }
    
    public string Name
    {
        get => name;
        set => name = value;
    }

    public ParameterType ParameterType 
    {
        get => parameterType;
        private set => parameterType = value;
    }

    public float Value
    {
        get => value;
        set => this.value = value;
    }

    public int BuffLevel
    {
        get => buffLevel;
        set => buffLevel = Mathf.Clamp(value, 0, maxBuffLevel); 
    }
    
    public int MaxBuffLevel => maxBuffLevel; 
}