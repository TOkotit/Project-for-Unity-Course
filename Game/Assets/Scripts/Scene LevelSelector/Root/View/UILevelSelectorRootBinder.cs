using Assets.Scripts.Levels;
using System;
using UnityEngine;

public class UILevelSelectorRootBinder : MonoBehaviour
{
    public event Action GoToFirstLevelButtonClicked;

    public void OnClickFirstLevel()
    {
        GoToFirstLevelButtonClicked?.Invoke();
    }
}
