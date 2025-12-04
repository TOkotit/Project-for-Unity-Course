using System.Collections;
using Scripts.GamePlar.Root;
using Scripts.Utils;
using System_Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System_Scripts.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRootView;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AfterStart()
        {
            
            var currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != Scenes.BOOT)
            {
                return;
            }
            
            Application.targetFrameRate = 60;
            
            
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
            
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRootView = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);
            
        }

        private void RunGame() 
        {
            #if UNITY_EDITOR
                var sceneName = SceneManager.GetActiveScene().name;
                if (sceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndSrartGame());

                    return;
                }

                if (sceneName != Scenes.BOOT)
                {
                    return;
                }
            #endif

            _coroutines.StartCoroutine(LoadAndSrartGame());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator LoadAndSrartGame()
        {
            
            _uiRootView.ShowLoadingScreen();
            
            yield return LoadScene(Scenes.GAMEPLAY);
            
            
            yield return new WaitForSeconds(2f);
            yield return null;
             
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEnrtyPoint>();
            
            
            if (sceneEntryPoint != null)
            {
                sceneEntryPoint.Run();
            }
            else
            {
                Debug.LogError("Ошибка: Не найдена точка входа в сцену Gameplay!");
            }
            GameManager.Instance.SetState(GameState.Gameplay);
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}