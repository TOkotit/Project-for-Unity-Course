using System.Collections;
using System.Collections.Generic;
using System_Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Музыка")]
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameplayMusic;

        [Header("Звуковые эффекты (SFX)")]
        public AudioClip buttonClick;
        public AudioClip enemySpawn;
        public AudioClip enemyDie;
        public AudioClip levelComplete;
        public AudioClip playerShoot;
        public AudioClip playerHit;
        public AudioClip playerDeath;

        [Header("Настройки пула")]
        [SerializeField] private int _sfxPoolSize = 8;

        private AudioSource _musicSource;
        private readonly Queue<AudioSource> _sfxPool = new();
        private GameObject _sfxPoolRoot;
        private AudioMixer _masterMixer;
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeAudioSystem();
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Debug.LogWarning("Что-то сильно пошло не так");
        }

        private void InitializeAudioSystem()
        {
            _masterMixer = Resources.Load<AudioMixer>("MasterMixer");
            if (_masterMixer == null)
                Debug.LogError("Микшер не найден в Resources");

            var musicGo = new GameObject("[Music Source]");
            _musicSource = musicGo.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
            if (_masterMixer != null)
            {
                var musicGroup = _masterMixer.FindMatchingGroups("Music");
                if (musicGroup.Length > 0)
                    _musicSource.outputAudioMixerGroup = musicGroup[0];
                else
                    Debug.LogError("Группа Music не найдена в Микшере");
            }
            DontDestroyOnLoad(musicGo);

            _sfxPoolRoot = new GameObject("[SFX Pool]");
            DontDestroyOnLoad(_sfxPoolRoot);

            for (int i = 0; i < _sfxPoolSize; i++)
            {
                var go = new GameObject($"SFX_Source_{i}");
                go.transform.SetParent(_sfxPoolRoot.transform);
                var source = go.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.spatialBlend = 1f;
                source.minDistance = 5f;
                source.maxDistance = 50f;

                if (_masterMixer != null)
                {
                    var sfxGroup = _masterMixer.FindMatchingGroups("SFX");
                    if (sfxGroup.Length > 0)
                        source.outputAudioMixerGroup = sfxGroup[0];
                    else
                        Debug.LogError("Группа SFX не найдена в Микшере");
                }

                _sfxPool.Enqueue(source);
            }

            LoadVolumeSettings();
        }

        public void PlayMenuMusic()
        {
            PlayMusic(_menuMusic, fade: true);
        }

        public void PlayPlayerDeathMusic()
        {
            PlayMusic(playerDeath, fade: false);
        }
        
        public void PlayGameplayMusic()
        {
            PlayMusic(_gameplayMusic, fade: true);
        }

        private void PlayMusic(AudioClip clip, bool fade = false)
        {
            if (_musicSource == null || clip == null) return;

            if (_musicSource.clip == clip && _musicSource.isPlaying) return;

            if (fade && _musicSource.isPlaying)
            {
                StartCoroutine(FadeAndPlayMusic(clip));
            }
            else
            {
                _musicSource.clip = clip;
                _musicSource.Play();
            }
        }

        private IEnumerator FadeAndPlayMusic(AudioClip newClip)
        {
            float startVolume = GetMusicVolume();
            for (float t = 0; t < 1f; t += Time.unscaledDeltaTime)
            {
                float vol = Mathf.Lerp(startVolume, 0f, t);
                SetMusicVolume(vol);
                yield return null;
            }

            _musicSource.clip = newClip;
            _musicSource.Play();

            for (float t = 0; t < 1f; t += Time.unscaledDeltaTime)
            {
                float vol = Mathf.Lerp(0f, startVolume, t);
                SetMusicVolume(vol);
                yield return null;
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;

            while (_sfxPool.Count > 0)
            {
                var source = _sfxPool.Dequeue();
                if (source == null || source.gameObject == null)
                    continue;

                source.clip = clip;
                source.Play();
                StartCoroutine(RecycleAfterDelay(source, clip.length));
                return;
            }

            Debug.LogWarning($"Пул звуков истощен");
        }

        private IEnumerator RecycleAfterDelay(AudioSource source, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);

            if (source != null && source.gameObject != null)
            {
                source.Stop();
                _sfxPool.Enqueue(source);
            }
        }

        public void SetMusicVolume(float normalizedValue)
        {
            normalizedValue = Mathf.Clamp01(normalizedValue);
            PlayerPrefs.SetFloat("MusicVolume", normalizedValue);
            ApplyVolume("MusicVolume", normalizedValue);
        }

        public void SetSFXVolume(float normalizedValue)
        {
            normalizedValue = Mathf.Clamp01(normalizedValue);
            PlayerPrefs.SetFloat("SFXVolume", normalizedValue);
            ApplyVolume("SFXVolume", normalizedValue);
        }

        private void ApplyVolume(string paramName, float normalizedValue)
        {
            if (_masterMixer == null) return;

            float dB = normalizedValue > 0 ? Mathf.Log10(normalizedValue) * 20f : -80f;
            _masterMixer.SetFloat(paramName, dB);
        }

        private float GetMusicVolume()
        {
            if (_masterMixer == null) return 1f;
            _masterMixer.GetFloat("MusicVolume", out float dB);
            return dB > -79.9f ? Mathf.Pow(10f, dB / 20f) : 0f;
        }

        private void LoadVolumeSettings()
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
            SetMusicVolume(musicVol);
            SetSFXVolume(sfxVol);
        }
    }
}