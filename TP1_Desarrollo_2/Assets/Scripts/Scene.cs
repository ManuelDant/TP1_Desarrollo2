using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string secondGameSceneName = "Game 2";
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private string tutorialSceneName = "Tutorial";

    public void ChangeMenuScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }

    public void ChangeCreditsScene()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(creditsSceneName);
    }

    public void ChangeChallengeScene()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(secondGameSceneName);
    }
    public void ChangeSceneGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(gameSceneName);
    }

    public void ChangeSceneTutorial()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(tutorialSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
