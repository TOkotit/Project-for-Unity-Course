using Scripts.Entities;
using TMPro;
using UnityEngine;
using System;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; 
    
    private Player player;

    private void Start()
    {
        player = Game.Instance.PlayerModel; 

        player.CurrentHpChanged.AddListener(UpdateHealthText);

        UpdateHealthText(player.CurrentHp, player.MaxHp);
    }
    
    private void UpdateHealthText(float hp, float maxHp)
    {
        scoreText.text = $"{Math.Round(hp)}/{maxHp}";
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.CurrentHpChanged.RemoveListener(UpdateHealthText);
        }
    }
}
