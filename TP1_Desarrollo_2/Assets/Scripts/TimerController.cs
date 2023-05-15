using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField]
    private float TimerSeconds;
    private float startTime;
    private float timeRemaining;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        timeRemaining = TimerSeconds - (Time.time - startTime);
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = "Time remaining: " + timeString;

        if (timeRemaining <= 0f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
    }
}
