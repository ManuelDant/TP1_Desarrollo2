using UnityEngine;

/// <summary>
/// Manager Health and Particle of the enemy
/// </summary>
public class EnemyHealthManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int life = 100;

    [Header("Particle")]
    [SerializeField] private ParticleSystem particle;

    private TimerController enemyCountController;
    private int enemyCount = 0;

    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    private void Start()
    {
        enemyCountController = FindObjectOfType<TimerController>();
    }

    /// <summary>
    /// Takes damage and updates the enemy's life. Destroys the enemy if life reaches zero.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take.</param>
    public void TakeDamage(int damageAmount)
    {
        particle.Play();
        EnemySoundManager enemySound = gameObject.GetComponent<EnemySoundManager>();
        enemySound.PlaySound();

        life -= damageAmount;
        if (life <= 0)
        {
            enemyCountController.EnemyKilled();

            Destroy(gameObject);
        }
    }
}
