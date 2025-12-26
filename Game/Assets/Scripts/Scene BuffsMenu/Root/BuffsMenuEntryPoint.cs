using System;
using System_Scripts.GameRoot;
using UnityEngine;

public class BuffsMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIBuffsMenuRootBinder _sceneUIRootPrefab;
    public static BuffsMenuEntryPoint Instance { get; private set; }    
    public event Action GoToLevelSelectSceneRequested;

    private void Awake()
    {
        Instance = this;
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
    
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
