using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnClickStart);
        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnClickOptions);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
    }
    private void OnClickStart()
    {
        Debug.Log("Нажата кнопка новая игра");
        SceneManager.LoadScene("Game");
    }
    private void OnClickOptions()
    {
        Debug.Log("Нажата кнопка настройки");
    }
    private void OnClickExit()
    {
        Debug.Log("Нажата кнопка выхода");
        Application.Quit();
        Debug.Log("Пользователь вышел из игры");
    }
}
