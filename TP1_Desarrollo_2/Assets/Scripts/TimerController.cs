using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private GameObject winPlane;
    [SerializeField] private GameObject losePlane;
    [SerializeField] private bool isTimer = true;

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
            enemy.GetComponent<EnemyController>().SetEnemyCount(EnemyCount);
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

            if (timeRemaining <= 1f)
            {
                losePlane.SetActive(true);
                DesactivateObjects();
            }
        }      
    }

    public void EnemyKilled()
    {
        enemiesLeft--;
        enemiesKilled++;
        UpdateEnemiesCountText();

        if (enemiesKilled >= EnemyCount)
        {
            winPlane.SetActive(true);
            DesactivateObjects();
            Time.timeScale = 0;
        }
    }

    private void UpdateEnemiesCountText()
    {
        enemiesCount.text = "All Enemies Left: " + enemiesLeft.ToString() + "\nEnemies to Defeat: " + enemiesKilled.ToString() + "/" + EnemyCount.ToString();
    }

    private void DesactivateObjects()
    {     
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject pause = GameObject.FindGameObjectWithTag("Menu");
        GameObject cameraPlayer = GameObject.FindGameObjectWithTag("Camera");
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<PlayerInput>().enabled = false;
        }
        player.GetComponent<PlayerInput>().enabled = false;
        pause.GetComponent<PlayerInput>().enabled = false;
        if (cameraPlayer)
        {
            cameraPlayer.GetComponent<PlayerInput>().enabled = false;
        }    
        enemiesCount.gameObject.SetActive(false);
    }
}
