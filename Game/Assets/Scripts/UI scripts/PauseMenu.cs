using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private PlayerInput playerInput;
    private bool isPaused;

    private void Awake()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(OnClickPause);
        if (resumeButton != null)
            resumeButton.onClick.AddListener(OnClickResume);
        if (menuButton != null)
            menuButton.onClick.AddListener(OnClickMenu);

        if (playerInput != null)
        {
            playerInput.onActionTriggered += OnPause;
        }
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (context.action.name == "Pause" && context.performed)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnClickPause()
    {
        Debug.Log("Нажата кнопка паузы");
        Pause();
    }

    private void OnClickResume()
    {
        Debug.Log("Нажата кнопка продолжить");
        Resume();
    }

    private void OnClickMenu()
    {
        Debug.Log("Нажата кнопка меню");
        LoadMenu();
    }

    private void Resume()
    {
        panelMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        panelMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}