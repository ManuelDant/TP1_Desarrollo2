using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void ChangeSceneGame()
    {
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void ChangeSceneMenu()
    {
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Menu");
    }

    public void ChangeSceneCredits()
    {
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Credits");
    }

    public void ChangeSceneTutorial()
    {
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
