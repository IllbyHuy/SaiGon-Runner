using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button ChooseLevelButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button GuideButton;
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button CloseButton;
    [SerializeField] private GameObject LevelSelectionPanel;
    private void Awake()
    {
        PlayButton.onClick.AddListener(() => {
            ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
        });

        ChooseLevelButton.onClick.AddListener(() => {
            ShowLevelSelection();
        });

        ExitButton.onClick.AddListener(() => { 
            Application.Quit();
        });

        GuideButton.onClick.AddListener(() => { 
            ScenesLoader.LoadScenes(ScenesLoader.Scene.GuideScene);
        });

        Level1Button.onClick.AddListener(() => LoadLevel(1));
        Level2Button.onClick.AddListener(() => LoadLevel(2));
        Level3Button.onClick.AddListener(() => LoadLevel(3));
        CloseButton.onClick.AddListener(CloseLevelSelection);
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
        LevelSelectionPanel.SetActive(false);
    }
}

