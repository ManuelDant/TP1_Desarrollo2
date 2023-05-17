using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void ChangeSceneGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void ChangeSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeSceneCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ChangeSceneTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
