using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button ChooseLevelButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button GuideButton;

    [Header("Level Selection UI")]
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button CloseButton;
    [SerializeField] private GameObject LevelSelectionPanel;

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
            ShowLevelSelection();
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

        if (Level1Button != null) Level1Button.onClick.AddListener(() => LoadLevel(1));
        if (Level2Button != null) Level2Button.onClick.AddListener(() => LoadLevel(2));
        if (Level3Button != null) Level3Button.onClick.AddListener(() => LoadLevel(3));
        if (CloseButton != null) CloseButton.onClick.AddListener(CloseLevelSelection);
    }

    private void ShowLevelSelection()
    {
        if (LevelSelectionPanel != null)
        {
            LevelSelectionPanel.SetActive(true);
        }
    }

    public void CloseLevelSelection()
    {
        if (LevelSelectionPanel != null)
        {
            LevelSelectionPanel.SetActive(false); // Ẩn Panel
        }
    }

    public void LoadLevel(int levelIndex)
    {
        switch (levelIndex)
        {
            case 1:
                ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
                break;
            case 2:
                ScenesLoader.LoadScenes(ScenesLoader.Scene.Map2v1);
                break;
            case 3:
                ScenesLoader.LoadScenes(ScenesLoader.Scene.Map3);
                break;
            default:
                ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
                break;
        }
        if (LevelSelectionPanel != null)
            LevelSelectionPanel.SetActive(false);
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
