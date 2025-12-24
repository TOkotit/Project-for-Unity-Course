using System;
using Entities.PlayerScripts;
using Scripts.Entities;
using TMPro;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private PlayerController player;

    public void Start()
    {
        player.PlayerModel.CurrentHpChanged.AddListener(UpdateHP);
        UpdateHP(player.PlayerModel.CurrentHp, player.PlayerModel.MaxHp);
    }

    public void UpdateHP(float hp, float maxHp)
    {
        healthText.text = $"{Math.Round(hp)}/{maxHp}";
    }
}
