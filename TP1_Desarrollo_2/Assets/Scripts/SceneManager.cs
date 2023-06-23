using UnityEngine;

/// <summary>
/// Controller of All Scenes of the game
/// </summary>
public class SceneManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string secondGameSceneName = "Game 2";
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private string tutorialSceneName = "Tutorial";

    /// <summary>
    /// Load Menu Scene
    /// </summary>
    public void ChangeMenuScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneName);
    }

    /// <summary>
    /// Load Credits Scene
    /// </summary>
    public void ChangeCreditsScene()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(creditsSceneName);
    }

    /// <summary>
    /// Load Challenge Scene
    /// </summary>
    public void ChangeChallengeScene()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.SceneManagement.SceneManager.LoadScene(secondGameSceneName);
    }

    /// <summary>
    /// Load Game Scene
    /// </summary>
    public void ChangeSceneGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>
    /// Load Tutorial Scene
    /// </summary>
    public void ChangeSceneTutorial()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.SceneManagement.SceneManager.LoadScene(tutorialSceneName);
    }

    /// <summary>
    /// Close the Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
