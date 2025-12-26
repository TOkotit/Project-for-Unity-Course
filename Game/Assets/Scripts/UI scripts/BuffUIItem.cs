using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button upgradeButton;

    private int _buffIndex;
    private BuffSystemModel _model;

    public void Setup(int index, Buff buff, BuffSystemModel model)
    {
        _buffIndex = index;
        _model = model;

        nameText.text = buff.Name;
        UpdateUI(buff);

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(OnUpgradeClick);
    }

    private void OnUpgradeClick()
    {
        _model.LevelUpBuff(_buffIndex);
        UpdateUI(_model.Buffs[_buffIndex]);
    }

    private void UpdateUI(Buff buff)
    {
        levelText.text = $"{buff.BuffLevel} / {buff.MaxBuffLevel}";
        
        upgradeButton.interactable = buff.BuffLevel < buff.MaxBuffLevel;
    }
}
