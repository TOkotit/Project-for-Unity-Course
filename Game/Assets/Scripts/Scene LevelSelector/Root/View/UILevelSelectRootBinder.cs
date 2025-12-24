using Assets.Scripts.Audio;
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

    public event Action GoToMainMenuButtonClicked;
    
    public event Action GoToBuffsMenuButtonClicked;

    public void OnClickBack()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        }
        else
        {
            Debug.LogWarning("AudioManager уничтожен, звук не воспроизведён.");
        }
        GoToMainMenuButtonClicked?.Invoke();
    }

    public void OnClickFirstLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToFirstLevelButtonClicked?.Invoke();
    }

    public void OnClickSecondLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToSecondLevelButtonClicked?.Invoke();
    }

    public void OnClickThirdLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToThirdLevelButtonClicked?.Invoke();
    }

    public void OnClickFourthLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToFourthLevelButtonClicked?.Invoke();
    }

    public void OnClickFifthLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToFifthLevelButtonClicked?.Invoke();
    }

    public void OnClickBuffsMenu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        GoToBuffsMenuButtonClicked?.Invoke();
    }
}
