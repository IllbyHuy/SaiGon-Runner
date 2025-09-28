using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("=== GAME UI ===")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("=== GAME STATE ===")]
    private int score = 0;
    private bool isGameOver = false;
    private bool isGameWin = false;

    void Start()
    {
        UpdateScore();
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);

        // Phát background music khi bắt đầu game
        PlayBackgroundMusic();
    }

    void Update()
    {

    }

    #region SCORE MANAGEMENT
    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();

            // Phát sound effect khi thu thập điểm
            PlayCollectSound();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    #endregion

    #region GAME STATE MANAGEMENT
    public void GameOver()
    {
        if (isGameOver) return; // Tránh gọi nhiều lần

        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        // Phát lost music
        PlayLostMusic();
    }

    public void GameWin()
    {
        if (isGameWin) return; // Tránh gọi nhiều lần

        isGameWin = true;
        gameWinUI.SetActive(true);
        Time.timeScale = 0f;
        finalScoreText.text = "Your score: " + score.ToString();

        // Phát win music
        PlayWinMusic();
    }

    public void RestartGame()
    {
        ResetGameState();
        PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        ResetGameState();
        PlayButtonSound();
        PlayMenuMusic(); // Phát menu music khi quay về menu
        SceneManager.LoadScene("MainMenuScenes");
    }

    public void NextLevel()
    {
        ResetGameState();
        PlayButtonSound();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Kiểm tra xem có scene tiếp theo không
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // Nếu không có scene tiếp theo, quay về MainMenu
            BackToMainMenu();
        }
    }

    private void ResetGameState()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isGameWin = false;
    }
    #endregion

    #region SCENE LOADING
    public void LoadMap1()
    {
        LoadScene("Map1");
    }

    public void LoadMap2()
    {
        LoadScene("Map2");
    }

    public void LoadMap3()
    {
        LoadScene("Map3");
    }

    public void LoadMap4()
    {
        Debug.Log("Chuyển từ Map3 sang Map4 - Cutscene hoàn thành!");
        LoadScene("Map4");
    }

    public void LoadMap4FromCutscene()
    {
        Debug.Log("Cutscene Map3 hoàn thành, chuyển sang Map4!");
        LoadScene("Map4");
    }

    private void LoadScene(string sceneName)
    {
        ResetGameState();
        PlayButtonSound();
        SceneManager.LoadScene(sceneName);
    }
    #endregion

    #region AUDIO INTEGRATION
    public void PlayBackgroundMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }

    public void PlayMenuMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMenuMusic();
        }
    }

    public void PlayWinMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayWinMusic();
        }
    }

    public void PlayLostMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayLostMusic();
        }
    }

    public void PlayButtonSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonClickSound();
        }
    }

    public void PlayCollectSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayCollectSound();
        }
    }

    public void PlayJumpSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayJumpSound();
        }
    }

    public void PlayRunSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayRunSound();
        }
    }

    public void PlayEnemyHitSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayEnemyHitSound();
        }
    }

    public void PlayPlayerHitSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayPlayerHitSound();
        }
    }

    public void PlayTrainSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayTrainSound();
        }
    }

    public void StopMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }
    }
    #endregion

    #region VOLUME CONTROL (có thể gọi từ Settings UI)
    public void SetMasterVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMasterVolume(volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(volume);
        }
    }

    public void SetUIVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetUIVolume(volume);
        }
    }
    #endregion

    #region GETTERS
    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;
    public int GetScore() => score;
    #endregion

    #region PLAYER ACTIONS (để gọi từ PlayerController)
    public void OnPlayerJump()
    {
        PlayJumpSound();
    }

    public void OnPlayerRun()
    {
        PlayRunSound();
    }

    public void OnPlayerHit()
    {
        PlayPlayerHitSound();
        // Có thể thêm logic giảm máu, hiệu ứng...
    }

    public void OnEnemyHit()
    {
        PlayEnemyHitSound();
        // Có thể thêm logic xử lý khi đánh kẻ thù
    }

    public void OnItemCollect(int points = 10)
    {
        AddScore(points); // Đã có PlayCollectSound() trong AddScore()
    }
    #endregion
}