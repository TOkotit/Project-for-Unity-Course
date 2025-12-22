using Assets.Scripts.Levels;
using System;
using UnityEngine;

public class UILevelSelectRootBinder : MonoBehaviour
{
    public event Action GoToFirstLevelButtonClicked;
    public event Action GoToSecondLevelButtonClicked;
    public event Action GoToThirdLevelButtonClicked;
    public event Action GoToFourthLevelButtonClicked;
    public event Action GoToFifthLevelButtonClicked;

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
}
