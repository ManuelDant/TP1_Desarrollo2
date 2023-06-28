using UnityEngine;

/// <summary>
/// Manager sound of the enemy
/// </summary>
public class EnemySoundManager : MonoBehaviour
{
    private AudioSource hitEnemySound;

    private void Start()
    {
        hitEnemySound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the enemy's hit sound.
    /// </summary>
    public void PlaySound()
    {
        hitEnemySound.Play();
    }
}
