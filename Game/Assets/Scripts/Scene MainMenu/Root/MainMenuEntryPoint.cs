using System;
using System_Scripts.GameRoot;
using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        public event Action GoToGameplaySceneRequested;

        private void Awake()
        {
            if (Game.Instance == null)
            {
                Game.Initialize();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void Run(UIRootView uiRoot)
        {
            Debug.Log("GameplayEntryPoint");
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            uiScene.GoToGameplayButtonClicked += () =>
            {
                GoToGameplaySceneRequested?.Invoke();
            };
        }
    }
}