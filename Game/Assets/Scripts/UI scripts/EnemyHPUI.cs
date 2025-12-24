using System;
using TMPro;
using UnityEngine;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Enemy enemy;

    public void Start()
    {
        enemy.Model.CurrentHpChanged.AddListener(UpdateHP);
        UpdateHP(enemy.Model.CurrentHp, enemy.Model.MaxHp);
    }

    public void UpdateHP(float hp, float maxHp)
    {
        healthText.text = $"{Math.Round(hp)}/{maxHp}";
    }
}
