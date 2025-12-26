using System;
using UnityEngine;
using UnityEngine.Events;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToLevelSelectButtonClicked;
    public event  Action ContinueButtonClicked;
    public event Action QuitButtonClicked;
    [SerializeField] private UnityEngine.UI.Button continueButton;
    
    
    public void SetContinueButtonInteractable(bool interactable)
    {
        if (continueButton != null)
            continueButton.interactable = interactable;
    }
    
    public void OnClickNewGame()
    {
        GoToLevelSelectButtonClicked?.Invoke();
    }
    
    public void OnClickContinue()
    {
        ContinueButtonClicked?.Invoke();
    }
    
    public void OnClickQuit()
    {
        QuitButtonClicked?.Invoke();
    }
    
}
