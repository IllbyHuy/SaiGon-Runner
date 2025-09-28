
using UnityEngine;
using UnityEngine.UI;   

public class GuideManage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Button BackButton;
    private void Awake()
    {
        BackButton.onClick.AddListener(() =>
        {
            ScenesLoader.LoadScenes(ScenesLoader.Scene.MainMenuScenes);
        });
    }
    }
