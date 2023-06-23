using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller that uses the inputs for the game pause logic
/// </summary>
public class PauseInputReaderManager : MonoBehaviour
{
    [Header("Setting Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject buttonReanude;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject weapon2;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject tutorialText;

    /// <summary>
    /// Pause game when press input pause
    /// </summary>
    public void OnPause()
    {
        pausePanel.SetActive(true);
        buttonReanude.SetActive(true);

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

    /// <summary>
    /// Resume game
    /// </summary>
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        buttonReanude.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
}
