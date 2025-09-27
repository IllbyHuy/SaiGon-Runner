using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    public enum Scene
    {
        MainMenuScenes,
        GuideScene,
        Map1 ,           
        Map2 
    }

    public static void LoadScenes(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
