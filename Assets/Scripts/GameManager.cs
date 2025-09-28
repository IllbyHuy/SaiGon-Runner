using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    private bool isGameOver = false;
    private bool isGameWin = false;
    void Start()
    {
        UpdateScore();
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int points)
    {
        if(!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        
        isGameOver = true;
        score = 0;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void GameWin()
    {

        isGameWin = true;
        gameWinUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScenes");
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
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
    
    public void LoadMap3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map3");
    }
    
    public void LoadMap1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map1");
    }
    
    public void LoadMap2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map2");
    }
    
    public void LoadMap4()
    {
        Time.timeScale = 1f;
        Debug.Log("Chuyển từ Map3 sang Map4 - Cutscene hoàn thành!");
        SceneManager.LoadScene("Map4");
    }
    
    public void LoadMap4FromCutscene()
    {
        Time.timeScale = 1f;
        Debug.Log("Cutscene Map3 hoàn thành, chuyển sang Map4!");
        SceneManager.LoadScene("Map4");
    }
    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;   
}
