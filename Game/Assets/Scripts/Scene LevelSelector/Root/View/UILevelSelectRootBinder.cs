using Assets.Scripts.Levels;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectRootBinder : MonoBehaviour
{
    public event Action GoToFirstLevelButtonClicked;
    public event Action GoToSecondLevelButtonClicked;
    public event Action GoToThirdLevelButtonClicked;
    public event Action GoToFourthLevelButtonClicked;
    public event Action GoToFifthLevelButtonClicked;
    public event Action GoToMainMenuButtonClicked;
    public event Action GoToBuffsMenuButtonClicked;
    
    
    
    [Header("Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    [Header("Locks")]
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;

    public void OnClickBack()
    {
        GoToMainMenuButtonClicked?.Invoke();
    }

    public void OnClickFirstLevel()
    {
        GoToFirstLevelButtonClicked?.Invoke();
    }

    public void OnClickSecondLevel()
    {
        GoToSecondLevelButtonClicked?.Invoke();
    }

    public void OnClickThirdLevel()
    {
        GoToThirdLevelButtonClicked?.Invoke();
    }

    public void OnClickFourthLevel()
    {
        GoToFourthLevelButtonClicked?.Invoke();
    }

    public void OnClickFifthLevel()
    {
        GoToFifthLevelButtonClicked?.Invoke();
    }

    public void OnClickBuffsMenu()
    {
        GoToBuffsMenuButtonClicked?.Invoke();
    }
}
