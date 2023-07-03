using UnityEngine;

/// <summary>
/// Manager Health of the enemy
/// </summary>
public class EnemyHealthManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int life = 100;

    private TimerController enemyCountController;
    private EnemyParticleManager enemyParticleManager;
    private int enemyCount = 0;

    /// <summary>
    /// Get total count of enemys in the scene, Set total count of enemys in the scene.
    /// </summary>
    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    private void Start()
    {
        enemyCountController = FindObjectOfType<TimerController>();
        enemyParticleManager = GetComponent<EnemyParticleManager>();
    }

    /// <summary>
    /// Takes damage and updates the enemy's life. Destroys the enemy if life reaches zero.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take.</param>
    public void TakeDamage(int damageAmount)
    {
        enemyParticleManager.PlayParticle();
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
