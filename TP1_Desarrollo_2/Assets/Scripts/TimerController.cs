using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemiesCount;
    [SerializeField]
    private float TimerSeconds;
    [SerializeField]
    private int EnemyCount;
    [SerializeField]
    private bool isTimer = true;

    private float startTime;
    private float timeRemaining;
    private int enemiesLeft;
    private int enemiesKilled;

    void Start()
    {
        startTime = Time.time;
        enemiesKilled = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().SetEnemyCount(EnemyCount);
        }
        UpdateEnemiesCountText();
    }

    void Update()
    {
        if (isTimer)
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

    public void EnemyKilled()
    {
        enemiesLeft--;
        enemiesKilled++;
        UpdateEnemiesCountText();
    }

    private void UpdateEnemiesCountText()
    {
        enemiesCount.text = "All Enemies Left: " + enemiesLeft.ToString() + "\nEnemies to Defeat: " + enemiesKilled.ToString() + "/" + EnemyCount.ToString();
    }
}
