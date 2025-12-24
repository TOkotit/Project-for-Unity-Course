using Assets.Scripts.Levels;
using System;
using System_Scripts.GameRoot;
using System_Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectorEntryPoint : MonoBehaviour
{
    [SerializeField] private UILevelSelectRootBinder _sceneUIRootPrefab;
    public event Action<string> GoToGameplaySceneRequested;
    public event Action GoToMainMenuSceneRequested;
    
    public event Action GoToBuffsMenuSceneRequested;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToMainMenuButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };

        uiScene.GoToFirstLevelButtonClicked += () =>
        {
            TryStartLevel("Level_01");
        };

        uiScene.GoToSecondLevelButtonClicked += () =>
        {
            TryStartLevel("Level_02");
        };

        uiScene.GoToThirdLevelButtonClicked += () =>
        {
            TryStartLevel("Level_03");
        };

        uiScene.GoToFourthLevelButtonClicked += () =>
        {
            TryStartLevel("Level_04");
        };

        uiScene.GoToFifthLevelButtonClicked += () =>
        {
            TryStartLevel("Level_05");
        };

        uiScene.GoToBuffsMenuButtonClicked += () =>
        {
            GoToBuffsMenuSceneRequested?.Invoke();
        };
        
        RefreshButtonStates(uiScene);
    }

    private void TryStartLevel(string levelId)
    {
        if (LevelProgressManager.Instance.IsLevelUnlocked(levelId))
        {
            GoToGameplaySceneRequested?.Invoke(levelId);
        }
        else
        {
            Debug.Log($"Уровень {levelId} заблокирован. Пройдите предыдущий уровень.");
        }
    }
    private void RefreshButtonStates(UILevelSelectRootBinder uiScene)
    {
        SetButtonInteractable(uiScene, "Button_Level1", "Level_01");
        SetButtonInteractable(uiScene, "Button_Level2", "Level_02");
        SetButtonInteractable(uiScene, "Button_Level3", "Level_03");
        SetButtonInteractable(uiScene, "Button_Level4", "Level_04");
        SetButtonInteractable(uiScene, "Button_Level5", "Level_05");
    }

    private void SetButtonInteractable(UILevelSelectRootBinder binder, string buttonName, string levelId)
    {
        bool isUnlocked = LevelProgressManager.Instance.IsLevelUnlocked(levelId);
        
        var btnTransform = binder.transform.Find(buttonName);
        if (btnTransform != null)        {
            var btn = btnTransform.GetComponent<UnityEngine.UI.Button>();
            if (btn != null) btn.interactable = isUnlocked;
            var lockTransform = btnTransform.Find("LockIcon");
            if (lockTransform != null)
            {
                lockTransform.gameObject.SetActive(!isUnlocked);
            }
            else
            {
                Debug.LogWarning($"На кнопке {buttonName} не найден объект LockIcon!");
            }
        }
    }
}
