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
        pausePanel.SetActive(true);
        botonReanude.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        weapon.GetComponent<PlayerInput>().enabled = false;
        weapon2.GetComponent<PlayerInput>().enabled = false;
        if (player)
        {
            player.GetComponent<PlayerInput>().enabled = false;
        }
       

        if (tutorialText != null)
            tutorialText.GetComponent<TextMeshProUGUI>().enabled = false;
        
    }
        
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        botonReanude.SetActive(false);
        Cursor.visible = false; // Ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        Time.timeScale = 1;
        weapon.GetComponent<PlayerInput>().enabled = true;
        weapon2.GetComponent<PlayerInput>().enabled = true;
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

    public void ChangeChallengeScene()
    {
        SceneManager.LoadScene("Game2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
