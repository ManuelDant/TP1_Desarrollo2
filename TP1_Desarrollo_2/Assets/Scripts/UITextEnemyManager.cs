using UnityEngine;
using TMPro;

/// <summary>
/// Manages the UI text related to enemy information.
/// </summary>
public class UITextEnemyManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemiesCount;

    /// <summary>
    /// Updates the timer text with the specified time string.
    /// </summary>
    /// <param name="timeString">The formatted time string to display.</param>
    public void UpdateTimerText(string timeString)
    {
        timerText.text = "Time remaining: " + timeString;
    }

    /// <summary>
    /// Updates the enemies count text with the provided information.
    /// </summary>
    /// <param name="enemiesLeft">The number of enemies remaining.</param>
    /// <param name="enemiesKilled">The number of enemies killed.</param>
    /// <param name="enemyCount">The total number of enemies.</param>
    public void UpdateEnemiesCountText(int enemiesLeft, int enemiesKilled, int enemyCount)
    {
        enemiesCount.text = "All Enemies Left: " + enemiesLeft.ToString() + "\nEnemies to Defeat: " + enemiesKilled.ToString() + "/" + enemyCount.ToString();
    }
}
