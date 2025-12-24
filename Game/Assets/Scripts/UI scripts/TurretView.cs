using TMPro;
using UnityEngine;

public class TurretView : MonoBehaviour
{
    [SerializeField] private TMP_Text turretText; 
    
    private TurretSystemModel turretSystemModel;

    private void Start()
    {
        turretSystemModel = Game.Instance.TurretSystemModel; 

        turretSystemModel.OnTurretChanged.AddListener(UpdateTurretText);
        UpdateTurretText();
    }
    
    private void UpdateTurretText()
    {
        turretText.text = $"{turretSystemModel.CurrentTurret.TurretType}";
    }

    private void OnDestroy()
    {
        if (turretSystemModel != null)
        {
            turretSystemModel.OnTurretChanged.RemoveListener(UpdateTurretText);
        }
    }
}
