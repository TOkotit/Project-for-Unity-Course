using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton; //unactive
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject menuPanel;

    private void OnEnable()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnClickStart);
        else
            Debug.Log("Отсутствует префаб");

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnClickOptions);
        else
            Debug.Log("Отсутствует префаб");

        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
        else
            Debug.Log("Отсутствует префаб");
    }

    private void OnClickStart()
    {
        Debug.Log("Нажата кнопка новая игра");
    }
    private void OnClickOptions()
    {
        optionsPanel.SetActive(true);
        Debug.Log("Нажата кнопка настройки");
        menuPanel.SetActive(false);
    }
    private void OnClickExit()
    {
        Debug.Log("Нажата кнопка выхода");
        Application.Quit();
        Debug.Log("Пользователь вышел из приложения");
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnClickStart);
        optionsButton.onClick.RemoveListener(OnClickOptions);
        exitButton.onClick.RemoveListener(OnClickExit);
    }
}
