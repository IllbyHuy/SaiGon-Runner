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

    private void Start()
    {
        // Phát nhạc menu khi vào Main Menu
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMenuMusic();
        }
    }

    private void Awake()
    {
        PlayButton.onClick.AddListener(() => {
            // Phát âm thanh click button
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayButtonClickSound();
            }

            ScenesLoader.LoadScenes(ScenesLoader.Scene.Map1);
        });

        ChooseLevelButton.onClick.AddListener(() => {
            // Phát âm thanh click button
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayButtonClickSound();
            }

            // Logic choose level
        });

        ExitButton.onClick.AddListener(() => {
            // Phát âm thanh click button
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayButtonClickSound();
            }

            Application.Quit();
        });

        GuideButton.onClick.AddListener(() => {
            // Phát âm thanh click button
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayButtonClickSound();
            }

            ScenesLoader.LoadScenes(ScenesLoader.Scene.GuideScene);
        });
    }
}