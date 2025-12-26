using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Levels;
using System_Scripts.GameRoot;
using System_Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelectorEntryPoint : MonoBehaviour
{
    [SerializeField] private UILevelSelectRootBinder _sceneUIRootPrefab;
    
    [SerializeField] private List<LevelStats00> _levelConfigs = new (5);
    public static LevelSelectorEntryPoint Instance { get; private set; }    

    public event Action<string> GoToGameplaySceneRequested;
    public event Action GoToMainMenuSceneRequested;
    
    public event Action GoToBuffsMenuSceneRequested;

    public void Awake()
    {
        Instance = this;
    }

    public void Run(UIRootView uiRoot)
    {
        LevelProgressManager.Instance.LoadProgress();

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToFirstLevelButtonClicked += () => TryStartLevel("Level_01");
        uiScene.GoToSecondLevelButtonClicked += () => TryStartLevel("Level_02");
        uiScene.GoToThirdLevelButtonClicked += () => TryStartLevel("Level_03");
        uiScene.GoToFourthLevelButtonClicked += () => TryStartLevel("Level_04");
        uiScene.GoToFifthLevelButtonClicked += () => TryStartLevel("Level_05");
    
        uiScene.GoToMainMenuButtonClicked += () => GoToMainMenuSceneRequested?.Invoke();
        uiScene.GoToBuffsMenuButtonClicked += () => GoToBuffsMenuSceneRequested?.Invoke();

        RefreshButtonStates(uiScene);
        
        AudioManager.Instance.PlayMenuMusic();
    }

    private void TryStartLevel(string levelId)
    {
        if (LevelProgressManager.Instance.IsLevelUnlocked(levelId))
        {
            var index = int.Parse(levelId.Substring(levelId.Length - 2)) - 1;
            Debug.Log($"{index}, {index.GetType()}");
            Game.Instance.CurrentLevelConfig = _levelConfigs[int.Parse(levelId.Substring(levelId.Length - 2)) - 1];
            Game.Instance.CurrentLevelId = levelId;
            GoToGameplaySceneRequested?.Invoke(levelId);
        }
        else
        {
            Debug.Log($"Уровень {levelId} заблокирован. Пройдите предыдущий уровень.");
        }
    }
    private void RefreshButtonStates(UILevelSelectRootBinder uiScene)
    {
        SetButtonInteractable(uiScene.button1, uiScene.lock1, "Level_01");
        SetButtonInteractable(uiScene.button2, uiScene.lock2, "Level_02");
        SetButtonInteractable(uiScene.button3, uiScene.lock3, "Level_03");
        SetButtonInteractable(uiScene.button4, uiScene.lock4, "Level_04");
        SetButtonInteractable(uiScene.button5, uiScene.lock5, "Level_05");
    }

    private void SetButtonInteractable(Button btn, GameObject lockIcon, string levelId)
    {
        if (btn == null)
        {
            Debug.LogError($"<color=red>[UI Error]</color> Кнопка для {levelId} не привязана в префабе Биндер!");
            return;
        }

        var isUnlocked = LevelProgressManager.Instance.IsLevelUnlocked(levelId);
    
        btn.interactable = isUnlocked;
        Debug.Log($"<color=green>[UI Success]</color> Кнопка '{btn.name}' для {levelId} обновлена. Статус: {(isUnlocked ? "Открыта" : "Закрыта")}");

        if (lockIcon != null)
        {
            lockIcon.SetActive(!isUnlocked);
            Debug.Log($"<color=white>[UI Icon]</color> Иконка на кнопке '{btn.name}' успешно {(isUnlocked ? "СКРЫТА" : "ПОКАЗАНА")}");
        }
    }
    
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
