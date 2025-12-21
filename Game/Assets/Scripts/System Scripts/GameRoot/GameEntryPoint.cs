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
        private readonly Coroutines _coroutines;
        private readonly UIRootView _uiRoot;
        
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
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);

            if (Game.Instance == null)
                Game.Initialize();

        }

        private void RunGame() 
        {
            #if UNITY_EDITOR
                var sceneName = SceneManager.GetActiveScene().name;
                if (sceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay());

                    return;
                }

                if (sceneName == Scenes.MAIN_MENU)
                {
                    _coroutines.StartCoroutine(LoadAndStartMainMenu());

                    return;
                }

                if (sceneName == Scenes.LEVEL_SELECT)
                {
                    _coroutines.StartCoroutine(LoadAndStartLevelSelect());

                    return;
                }

                if (sceneName != Scenes.BOOT)
                    {
                        return;
                    }
            #endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator LoadAndStartGameplay()
        {
            
            _uiRoot.ShowLoadingScreen();
            
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2f);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            if (sceneEntryPoint)
            {
               sceneEntryPoint.Run(_uiRoot);
            }
            else
            {
                Debug.LogError("Ошибка: Не найдена точка входа в сцену Gameplay!");
            }

            sceneEntryPoint.GoToMainMenuSceneRequested += () =>
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            };

            GameManager.Instance.SetState(GameState.Gameplay);
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu()
        {

            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);


            yield return new WaitForSeconds(2f);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

            if (sceneEntryPoint)
            {
                sceneEntryPoint.Run(_uiRoot);
            }
            else
            {
                Debug.LogError("Ошибка: Не найдена точка входа в сцену MainMenu!");
            }

            sceneEntryPoint.GoToGameplaySceneRequested += () =>
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
            };

            GameManager.Instance.SetState(GameState.Menu);
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartLevelSelect()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.LEVEL_SELECT);


            yield return new WaitForSeconds(2f);

            var sceneEntryPoint = Object.FindFirstObjectByType<LevelSelectorEntryPoint>();

            if (sceneEntryPoint)
            {
                sceneEntryPoint.Run(_uiRoot);
            }
            else
            {
                Debug.LogError("Ошибка: Не найдена точка входа в сцену LevelSelect!");
            }

            sceneEntryPoint.GoToGameplaySceneRequested += () =>
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
            };

            GameManager.Instance.SetState(GameState.Menu);
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}