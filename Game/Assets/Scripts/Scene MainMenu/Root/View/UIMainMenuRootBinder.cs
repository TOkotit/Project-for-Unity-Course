using System;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToLevelSelectButtonClicked;

    public void OnClickNewGame()
    {
        GoToLevelSelectButtonClicked?.Invoke();
    }
}
