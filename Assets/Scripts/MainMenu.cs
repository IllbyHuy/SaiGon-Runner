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
    private void Awake()
    {
        PlayButton.onClick.AddListener(() => {
            ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
        });

        ChooseLevelButton.onClick.AddListener(() => { 

        });

        ExitButton.onClick.AddListener(() => { 
            Application.Quit();
        });

        GuideButton.onClick.AddListener(() => { 
            ScenesLoader.LoadScenes(ScenesLoader.Scene.GuideScene);
        });
    }
}

