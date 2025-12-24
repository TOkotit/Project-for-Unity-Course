using System;
using System_Scripts.GameRoot;
using UnityEngine;

public class BuffsMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIBuffsMenuRootBinder _sceneUIRootPrefab;

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

        uiScene.GoToLevelSelectButtonClicked += () =>
        {
            GoToLevelSelectSceneRequested?.Invoke();
        };
    }
}
