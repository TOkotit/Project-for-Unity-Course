using System;
using System_Scripts.GameRoot;
using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootBinder;

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
            Debug.Log("GameplayEntryPoint");

            var uiScene = Instantiate(_sceneUIRootBinder);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            uiScene.GoToMainMenuButtonClicked += () =>
            {
                GoToMainMenuSceneRequested?.Invoke();
            };
        }
    }
}