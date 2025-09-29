using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("=== AUDIO SOURCES ===")]
    [SerializeField] private AudioSource musicSource;     // Nhạc nền
    [SerializeField] private AudioSource sfxSource;       // Hiệu ứng âm thanh
    [SerializeField] private AudioSource uiSource;        // Âm thanh UI

    [Header("=== BACKGROUND MUSIC ===")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lost;
    [SerializeField] private AudioClip trainSound;


    [Header("=== GAME SOUND EFFECTS ===")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip playerHitSound;

    [Header("=== UI SOUNDS ===")]
    [SerializeField] private AudioClip buttonClickSound;

    [Header("=== VOLUME SETTINGS ===")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float uiVolume = 0.9f;

    // Singleton pattern
    public static AudioManager instance;

    void Awake()
    {
        // Tạo singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("✅ AudioManager created and set to DontDestroyOnLoad");
        }
        else
        {
            Debug.Log("❌ AudioManager duplicate destroyed!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Debug checks
        Debug.Log("=== AUDIO MANAGER DEBUG ===");
        Debug.Log("MusicSource: " + (musicSource != null ? "OK" : "NULL"));
        Debug.Log("SFXSource: " + (sfxSource != null ? "OK" : "NULL"));
        Debug.Log("UISource: " + (uiSource != null ? "OK" : "NULL"));
        Debug.Log("BackgroundMusic: " + (backgroundMusic != null ? "OK" : "NULL"));
        Debug.Log("MenuMusic: " + (menuMusic != null ? "OK" : "NULL"));
        Debug.Log("Master Volume: " + masterVolume);
        Debug.Log("Music Volume: " + musicVolume);

        // Phát nhạc nền khi bắt đầu
        PlayBackgroundMusic();
    }

    void Update()
    {
        // Cập nhật volume
        UpdateVolumes();
    }

    #region BACKGROUND MUSIC
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMenuMusic()
    {
        if (menuMusic != null && musicSource != null)
        {
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayWinMusic()
    {
        if (win != null && musicSource != null)
        {
            musicSource.clip = win;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayLostMusic()
    {
        if (lost != null && musicSource != null)
        {
            musicSource.clip = lost;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    #endregion

    #region GAME SOUND EFFECTS
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

    public void PlayRunSound()
    {
        PlaySFX(runSound);
    }

    public void PlayCollectSound()
    {
        PlaySFX(collectSound);
    }

    public void PlayTrainSound()
    {
        PlaySFX(trainSound);
    }

    public void PlayEnemyHitSound()
    {
        PlaySFX(enemyHitSound);
    }

    public void PlayPlayerHitSound()
    {
        PlaySFX(playerHitSound);
    }
    #endregion

    #region UI SOUNDS
    public void PlayButtonClickSound()
    {
        PlayUISound(buttonClickSound);
    }
    #endregion

    #region PRIVATE METHODS
    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void PlayUISound(AudioClip clip)
    {
        if (clip != null && uiSource != null)
        {
            uiSource.PlayOneShot(clip);
        }
    }

    private void UpdateVolumes()
    {
        if (musicSource != null)
            musicSource.volume = masterVolume * musicVolume;

        if (sfxSource != null)
            sfxSource.volume = masterVolume * sfxVolume;

        if (uiSource != null)
            uiSource.volume = masterVolume * uiVolume;
    }
    #endregion

    #region PUBLIC METHODS
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetUIVolume(float volume)
    {
        uiVolume = Mathf.Clamp01(volume);
    }
    #endregion
}