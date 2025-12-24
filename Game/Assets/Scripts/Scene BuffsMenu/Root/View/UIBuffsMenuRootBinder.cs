using System;
using UnityEngine;

public class UIBuffsMenuRootBinder : MonoBehaviour
{
    public event Action GoToLevelSelectButtonClicked;

    public void OnClickBack()
    {
        GoToLevelSelectButtonClicked?.Invoke();
    }
}
