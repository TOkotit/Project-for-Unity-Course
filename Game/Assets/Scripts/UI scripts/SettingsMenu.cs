using Assets.Scripts.Audio;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropDown;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown screenModeDropDown;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private TMP_Text percentageMusic;
    [SerializeField] private TMP_Text percentageSound;
    private Resolution[] resolutions;
    PlayerPrefLocaleSelector playerPrefLocaleSelector = new PlayerPrefLocaleSelector();

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject menuPanel;
    
    [SerializeField] private InputSaveManager inputSaveManager;
    [SerializeField] private Button openRebind;
    [SerializeField] private Button closeRebind;
    [SerializeField] private GameObject rebindMenu;
    
    private void Start()
    {
        languageDropDown.ClearOptions();
        var languages = new List<string>();
        var currentLanguageIndex = 1;

        for (var i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            languages.Add(LocalizationSettings.AvailableLocales.Locales[i].ToString());
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[i])
                currentLanguageIndex = i;
        }

        languageDropDown.AddOptions(languages);
        languageDropDown.RefreshShownValue();

        resolutionDropDown.ClearOptions();
        var options = new List<string>();
        resolutions = Screen.resolutions;
        var currentResolutionIndex = 0;

        for (var i = 0; i < resolutions.Length; i++)
        {
            var option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height))
                currentResolutionIndex = i;
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.RefreshShownValue();

        screenModeDropDown.ClearOptions();
        var allScreenModes = new[] {"ExclusiveFullScreen", "FullScreenWindow", "MaximizedWindow", "Windowed" };
        var modes = new List<string>();
        var currentScreenMode = 1;

        foreach (var mode in allScreenModes)
        {
            modes.Add(mode);
        }

        screenModeDropDown.AddOptions(modes);
        screenModeDropDown.RefreshShownValue();

        var musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        var sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        var currentSoundVolume = 1f;
        var currentMusicVolume = 1f;
        musicSlider.value = musicVolume;
        soundSlider.value = sfxVolume;

        UpdateMixerVolume("MusicVolume", musicVolume);
        UpdateMixerVolume("SFXVolume", sfxVolume);

        OnSetMusicValue(musicSlider.value);
        OnSetSoundValue(soundSlider.value);

        LoadSettings(currentLanguageIndex, currentResolutionIndex, currentScreenMode, currentSoundVolume, currentMusicVolume);
        OnSetMusicValue(musicSlider.value);
        OnSetSoundValue(soundSlider.value);
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
        
        if (openRebind != null)
            openRebind.onClick.AddListener(OnOpenRebindMenu);
        else
            Debug.Log("Отстутствует префаб");
        
        if (closeRebind != null)
            closeRebind.onClick.AddListener(OnCloseRebindMenu);
        else
            Debug.Log("Отстутствует префаб");
    }

    private void OnSetLanguage(int localeIndex)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        Debug.Log($"Язык изменен на: {LocalizationSettings.AvailableLocales.Locales[localeIndex].Identifier}");
    }


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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        ScreenModeFind(screenModeIndex);
        Debug.Log("Режим экрана изменен");
    }

    private void OnSetResolution(int resolutionIndex)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Разрешение изменено");
    }
    
    private void OnSetMusicValue(float musicValue)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        musicValue = Mathf.RoundToInt(musicValue * 100f) / 100f;
        var percent = Mathf.RoundToInt(musicValue * 100);
        percentageMusic.text = $"{percent}%";

        UpdateMixerVolume("MusicVolume", musicValue);
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
    }

    private void OnSetSoundValue(float soundValue)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        soundValue = Mathf.RoundToInt(soundValue * 100f) / 100f;
        var percent = Mathf.RoundToInt(soundValue * 100);
        percentageSound.text = $"{percent}%";

        UpdateMixerVolume("SFXVolume", soundValue);
        PlayerPrefs.SetFloat("SFXVolume", soundValue);
    }

    private void UpdateMixerVolume(string mixer, float value)
    {
        var dB = value > 0 ? Mathf.Log10(value) * 20 : -80;
        masterMixer.SetFloat(mixer, dB);
    }
    
    private void OnClickSave()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        PlayerPrefs.SetInt(playerPrefLocaleSelector.PlayerPreferenceKey, languageDropDown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropDown.value);
        PlayerPrefs.SetInt("FullscreenPreference", screenModeDropDown.value);
        PlayerPrefs.SetFloat("SFXVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        inputSaveManager.SaveBindingOverrides();
        Debug.Log("Изменения сохранены");
    }

    private void OnClickExit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        optionsPanel.SetActive(false);
        Debug.Log("Выход из меню настроек");
        menuPanel.SetActive(true);
    }

    private void OnOpenRebindMenu()
    {
        rebindMenu.SetActive(true);
    }
    
    private void OnCloseRebindMenu()
    {
        rebindMenu.SetActive(false);
    }


    private void LoadSettings(int currentLanguageIndex, int currentResolutionIndex, int currentScreenMode,
       float currentSoundVolume, float currentMusicVolume)
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

        if (PlayerPrefs.HasKey("SFXVolume"))
            soundSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        else
            soundSlider.value = currentSoundVolume;

        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        else
            musicSlider.value = currentMusicVolume;

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
