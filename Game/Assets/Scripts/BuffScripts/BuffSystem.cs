using System;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    private BuffSystemModel buffSystemModel;

    private void Awake()
    {
        buffSystemModel = Game.Instance.BuffModel;
    }
    private void Start()
    {
        buffSystemModel.ApplyAllBuffs();
    }
}
