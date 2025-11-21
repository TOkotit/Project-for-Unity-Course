using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown screenModeDropDown;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Text percentageMusic;
    [SerializeField] private TMP_Text percentageSound;
    private Resolution[] resolutions;

    private void Start()
    {
        //Загрузка всех разрешений
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height))
                currentResolutionIndex = i;
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.RefreshShownValue();

        //Загрузка всех режимов экрана
        screenModeDropDown.ClearOptions();
        string[] allScreenModes = new string[4] {"ExclusiveFullScreen", "FullScreenWindow", "MaximizedWindow", "Windowed" };
        List<string> modes = new List<string>();
        int currentScreenMode = 1;

        for (int i = 0; i < allScreenModes.Length; i++)
        {
            string mode = allScreenModes[i];
            modes.Add(mode);
        }

        screenModeDropDown.AddOptions(modes);
        screenModeDropDown.RefreshShownValue();
        LoadSettings(currentResolutionIndex, currentScreenMode);
    }

    private void Awake()
    {
        if (resolutionDropDown != null)
            resolutionDropDown.onValueChanged.AddListener(OnSetResolution);
        if (screenModeDropDown != null)
            screenModeDropDown.onValueChanged.AddListener(OnSetScreenMode);
        if (saveButton != null)
            saveButton.onClick.AddListener(OnClickSave);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(OnSetMusicValue);
        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSetSoundValue);
    }

    //Настройки Графики

    private void ScreenModeFind(int screenModeIndex)
    {
        switch (screenModeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    private void OnSetScreenMode(int screenModeIndex)
    {
        ScreenModeFind(screenModeIndex);
        Debug.Log("Режим экрана изменен");
    }

    private void OnSetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Разрешение изменено");
    }

    //Настройка музыки и звука

    private void OnSetMusicValue(float musicValue)
    {
        musicValue = Mathf.RoundToInt(musicValue);
        string stringMusicValue = musicValue.ToString();
        percentageMusic.text = $"{stringMusicValue}%";
        Debug.Log("Громкость музыки изменена");
    }

    private void OnSetSoundValue(float soundValue)
    {
        soundValue = Mathf.RoundToInt(soundValue);
        string stringSoundValue = soundValue.ToString();
        percentageSound.text = $"{stringSoundValue}%";
        Debug.Log("Громкость звука изменена");
    }

    //Прочие кнопки

    private void OnClickSave()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropDown.value);
        PlayerPrefs.SetInt("FullscreenPreference", screenModeDropDown.value);
        Debug.Log("Изменения сохранены");
    }

    private void OnClickExit()
    {
        Debug.Log("Выход из меню настроек");
    }


    //Загрузка старых настроек
    private void LoadSettings(int currentResolutionIndex, int currentScreenMode)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropDown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            screenModeDropDown.value = PlayerPrefs.GetInt("FullscreenPreference");
        else
            screenModeDropDown.value = currentScreenMode;
        Debug.Log("Старые настройки загружены");
    }
}
