using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button ChooseLevelButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button GuideButton;

    private void Start()
    {
        // Phát nhạc menu khi vào Main Menu
        PlayMenuMusic();
    }

    private void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            PlayButtonSound();
            ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
        });

        ChooseLevelButton.onClick.AddListener(() =>
        {
            PlayButtonSound();
            // Logic choose level
        });

        ExitButton.onClick.AddListener(() =>
        {
            PlayButtonSound();
            ExitGame();
        });

        GuideButton.onClick.AddListener(() =>
        {
            PlayButtonSound();
            ScenesLoader.LoadScenes(ScenesLoader.Scene.GuideScene);
        });
    }

    #region AUDIO METHODS
    private void PlayButtonSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonClickSound();
        }
    }

    private void PlayMenuMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMenuMusic();
        }
    }
    #endregion

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}