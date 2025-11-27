using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
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
        Debug.Log("������ ������ ����� ����");
        SceneManager.LoadScene("MainGame");
    }
    private void OnClickOptions()
    {
        Debug.Log("������ ������ ���������");
    }
    private void OnClickExit()
    {
        Debug.Log("������ ������ ������");
        Application.Quit();
        Debug.Log("������������ ����� �� ����");
    }
}
