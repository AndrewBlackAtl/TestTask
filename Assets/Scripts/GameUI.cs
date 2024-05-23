using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _restartScreen;

    private static GameUI i;
    public static GameUI I => i;
    
    private void Awake()
    {
        i = this;
    }

    public void ShowRestartScreen()
    {
        _restartScreen.SetActive(true);
    }

    public void OnRestartButtonClick()
    {
        SceneLoader.LoadGameScene();
    }
}
