using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private const string GameSceneName = "Game";
    
    
    public static void LoadGameScene()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}