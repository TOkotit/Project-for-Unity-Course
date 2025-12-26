using Assets.Scripts.Audio;
using Assets.Scripts.Levels;
using Levels;
using Scripts.Entities;
using System;
using System_Scripts.GameRoot;
using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootBinder;
        [SerializeField] private LevelController levelController;
        public event Action GoToMainMenuSceneRequested;
    
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
            var uiScene = Instantiate(_sceneUIRootBinder);
            uiRoot.AttachSceneUI(uiScene.gameObject);
            if (levelController != null)
            {
                levelController.OnExitRequested += () => 
                {
                    GoToMainMenuSceneRequested?.Invoke(); 
                };
            }
            
            uiScene.GoToMainMenuButtonClicked += () =>
            {
                GoToMainMenuSceneRequested?.Invoke();
            };

            AudioManager.Instance.PlayGameplayMusic();
        }
    }
}