using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    public enum Scene
    {
        MainMenu,
        SampleScene
    }

    public static void LoadScenes(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
