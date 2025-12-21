using System;
using System_Scripts.GameRoot;
using System_Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectorEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public event Action GoToGameplaySceneRequested;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

       // ui.OnLevelSelected += OnLevelSelected;
       // ui.RefreshView();
    }

    private void OnLevelSelected(string sceneName)
    {
        Debug.Log($"Выбран уровень: {sceneName}");

        // Передаём управление GameManager'у или GameEntryPoint
        // Вариант 1: через смену состояния
        //GameManager.Instance.SetState(GameState.Loading);

        // Вариант 2: запустить загрузку напрямую
        //StartCoroutine(LoadLevel(sceneName));
    }

    private System.Collections.IEnumerator LoadLevel(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        // Здесь можно вызвать GameplayEntryPoint.Run(...)
    }

}
