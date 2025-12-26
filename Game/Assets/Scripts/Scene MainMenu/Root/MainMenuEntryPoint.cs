using Assets.Scripts.Levels;
using Assets.Scripts.Audio;
using System;
using System_Scripts.GameRoot;
using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        public event Action GoToLevelSelectSceneRequested;

        private void Awake()
        {
            if (Game.Instance == null)
            {
                Game.Initialize();
                DontDestroyOnLoad(gameObject);
            }
        }
        public void Run(UIRootView uiRoot)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);
            
            var hasSave = LevelProgressManager.Instance.HasAnyProgress();
            uiScene.SetContinueButtonInteractable(hasSave);
            
            uiScene.GoToLevelSelectButtonClicked += () =>
            {
                LevelProgressManager.Instance.ClearProgress(); 
                Game.Instance.BuffModel.ClearBuffsSave();
                GoToLevelSelectSceneRequested?.Invoke();
            };
            
            uiScene.ContinueButtonClicked += () =>
            {
                GoToLevelSelectSceneRequested?.Invoke();
            };

            AudioManager.Instance.PlayMenuMusic();
        }
    }
}