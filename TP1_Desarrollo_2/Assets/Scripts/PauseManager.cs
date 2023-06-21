using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Setting Pause")]
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    //TODO: Fix - Code is in Spanish
    private GameObject botonReanude;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject weapon2;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject tutorialText;

    public void OnPause()
    {
        //TODO: TP2 - SOLID
        pausePanel.SetActive(true);
        botonReanude.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        weapon.GetComponent<PlayerInput>().enabled = false;
        
        if (player)
        {
            player.GetComponent<PlayerInput>().enabled = false;
        }
        if (weapon2)
        {
            weapon2.GetComponent<PlayerInput>().enabled = false;
        }

        if (tutorialText != null)
            tutorialText.GetComponent<TextMeshProUGUI>().enabled = false;
        
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        botonReanude.SetActive(false);
        //TODO: Fix - Code is in Spanish
        Cursor.visible = false; // Ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        Time.timeScale = 1;
        weapon.GetComponent<PlayerInput>().enabled = true;
        if (weapon2)
        {
            weapon2.GetComponent<PlayerInput>().enabled = true;
        }     
        if (player)
        {
            player.GetComponent<PlayerInput>().enabled = true;
        }

        if (tutorialText != null)
            tutorialText.GetComponent<TextMeshProUGUI>().enabled = true;

    }

    public void ChangeMenuScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ChangeCreditsScene()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;       
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Credits");
    }

    public void ChangeChallengeScene()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //TODO: Fix - Hardcoded value
        SceneManager.LoadScene("Game 2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
