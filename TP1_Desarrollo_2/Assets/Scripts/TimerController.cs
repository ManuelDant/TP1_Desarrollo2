using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Control the time of the game by information of the living enemies
/// </summary>
public class TimerController : MonoBehaviour
{
    [SerializeField] private float TimerSeconds;
    [SerializeField] private int EnemyCount;
    [SerializeField] private GameObject winPlane;
    [SerializeField] private GameObject losePlane;
    [SerializeField] private bool isTimer = true;

    private UITextEnemyManager uITextEnemyManager;
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
            enemy.GetComponent<EnemyHealthManager>().EnemyCount = EnemyCount;
        }
        uITextEnemyManager = GetComponent<UITextEnemyManager>();
        uITextEnemyManager.UpdateEnemiesCountText(enemiesLeft, enemiesKilled, EnemyCount);
    }

    void Update()
    {
        if (isTimer)
        {
            timeRemaining = TimerSeconds - (Time.time - startTime);
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
            uITextEnemyManager.UpdateTimerText(timeString);

            if (timeRemaining <= 1f)
            {
                losePlane.SetActive(true);
                DesactivateObjects();
            }
        }
    }

    /// <summary>
    /// Called when an enemy is killed. Updates the enemy count and checks if all enemies are defeated.
    /// </summary>
    public void EnemyKilled()
    {
        enemiesLeft--;
        enemiesKilled++;
        uITextEnemyManager.UpdateEnemiesCountText(enemiesLeft, enemiesKilled, EnemyCount);

        if (enemiesKilled >= EnemyCount)
        {
            winPlane.SetActive(true);
            DesactivateObjects();
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Deactivates objects and disables player input.
    /// </summary>
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
        uITextEnemyManager.enemiesCount.gameObject.SetActive(false);
    }
}
