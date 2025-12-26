using Assets.Scripts.Audio;
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        Debug.Log("Нажата кнопка новая игра");
    }

    private void OnClickContinue()
    {
        Debug.Log("Нажата кнопка продолжить");
    }
    private void OnClickOptions()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        optionsPanel.SetActive(true);
        Debug.Log("Нажата кнопка настройки");
        menuPanel.SetActive(false);
    }
    private void OnClickExit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        Debug.Log("Нажата кнопка выхода");
        Application.Quit();
        Debug.Log("Пользователь вышел из приложения");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnClickStart);
        optionsButton.onClick.RemoveListener(OnClickOptions);
        exitButton.onClick.RemoveListener(OnClickExit);
    }
}
