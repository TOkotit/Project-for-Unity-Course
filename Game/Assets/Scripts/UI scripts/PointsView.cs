using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; 
    
    private BuffSystemModel buffModel;

    private void Start()
    {
        buffModel = Game.Instance.BuffModel; 

        buffModel.PointsChanged.AddListener(UpdateScoreText);

        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = $"{buffModel.Points}";
    }

    private void OnDestroy()
    {
        if (buffModel != null)
        {
            buffModel.PointsChanged.RemoveListener(UpdateScoreText);
        }
    }
}
