using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropDown;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown screenModeDropDown;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Text percentageMusic;
    [SerializeField] private TMP_Text percentageSound;
    private Resolution[] resolutions;
    PlayerPrefLocaleSelector playerPrefLocaleSelector = new PlayerPrefLocaleSelector();

    private void Start()
    {
        //Загрузка языков
        languageDropDown.ClearOptions();
        List<string> languages = new List<string>();
        int currentLanguageIndex = 1;


        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            languages.Add(LocalizationSettings.AvailableLocales.Locales[i].ToString());
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[i])
                currentLanguageIndex = i;
        }

        languageDropDown.AddOptions(languages);
        languageDropDown.RefreshShownValue();

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

        LoadSettings(currentLanguageIndex, currentResolutionIndex, currentScreenMode);
    }

    private void OnEnable()
    {
        if (languageDropDown != null)
            languageDropDown.onValueChanged.AddListener(OnSetLanguage);
        else
            Debug.Log("Отстутствует префаб");

        if (resolutionDropDown != null)
            resolutionDropDown.onValueChanged.AddListener(OnSetResolution);
        else
            Debug.Log("Отстутствует префаб");

        if (screenModeDropDown != null)
            screenModeDropDown.onValueChanged.AddListener(OnSetScreenMode);
        else
            Debug.Log("Отстутствует префаб");

        if (saveButton != null)
            saveButton.onClick.AddListener(OnClickSave);
        else
            Debug.Log("Отстутствует префаб");

        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
        else
            Debug.Log("Отстутствует префаб");

        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(OnSetMusicValue);
        else
            Debug.Log("Отстутствует префаб");

        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSetSoundValue);
        else
            Debug.Log("Отстутствует префаб");
    }

    //Настройки языка
    private void OnSetLanguage(int localeIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        Debug.Log($"Язык изменен на: {LocalizationSettings.AvailableLocales.Locales[localeIndex].Identifier}");
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
        PlayerPrefs.SetInt(playerPrefLocaleSelector.PlayerPreferenceKey, languageDropDown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropDown.value);
        PlayerPrefs.SetInt("FullscreenPreference", screenModeDropDown.value);
        Debug.Log("Изменения сохранены");
    }

    private void OnClickExit()
    {
        Debug.Log("Выход из меню настроек");
    }


    //Загрузка старых настроек

    private void LoadSettings(int currentLanguageIndex, int currentResolutionIndex, int currentScreenMode)
    {
        if (PlayerPrefs.HasKey(playerPrefLocaleSelector.PlayerPreferenceKey))
            languageDropDown.value = PlayerPrefs.GetInt(playerPrefLocaleSelector.PlayerPreferenceKey);
        else
            languageDropDown.value = currentLanguageIndex;

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

    private void OnDisable()
    {
        languageDropDown.onValueChanged.RemoveListener(OnSetLanguage);
        resolutionDropDown.onValueChanged.RemoveListener(OnSetResolution);
        screenModeDropDown.onValueChanged.RemoveListener(OnSetScreenMode);
        saveButton.onClick.RemoveListener(OnClickSave);
        exitButton.onClick.RemoveListener(OnClickExit);
        musicSlider.onValueChanged.RemoveListener(OnSetMusicValue);
        soundSlider.onValueChanged.RemoveListener(OnSetSoundValue);
    }
}
