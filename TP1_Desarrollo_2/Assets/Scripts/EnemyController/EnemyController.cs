using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int life = 100;

    [Header("Movement Settings")]
    [SerializeField] private EnemyMovementSetup.MovementStrategyType movementStrategy;
    
    [Header("Sound")]
    [SerializeField] private AudioSource hitEnemySound;

    [Header("Particle")]
    [SerializeField] private ParticleSystem particle;

    private TimerController enemyCountController;
    private IMovementManager movementManager;
    private int enemyCount = 0;

    /// <summary>
    /// Initializes the necessary components and assigns the timer controller.
    /// </summary>
    private void Start()
    {
        hitEnemySound.enabled = true;
        enemyCountController = FindObjectOfType<TimerController>();
        movementManager = GetComponent<IMovementManager>();

        SetMovementStrategy(movementStrategy); // Establecer la estrategia de movimiento inicial
    }

    /// <summary>
    /// Sets the enemy's movement strategy.
    /// </summary>
    /// <param name="strategy">The movement strategy to set.</param>
    public void SetMovementStrategy(EnemyMovementSetup.MovementStrategyType strategy)
    {
        movementStrategy = strategy;

        if (movementManager != null)
        {
            movementManager.OnMovementStrategyChanged(strategy);
        }
    }

    /// <summary>
    /// Sets the enemy's count in all scene
    /// </summary>
    /// <param name="enemycount">The count enemy´s in scene to set.</param>
    public void SetEnemyCount(int enemycount)
    {
        enemyCount = enemycount;
    }

    /// <summary>
    /// Plays the enemy's hit sound.
    /// </summary>
    public void PlaySound()
    {
        hitEnemySound.Play();
    }

    /// <summary>
    /// Takes damage and updates the enemy's life. Destroys the enemy if life reaches zero.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take.</param>
    public void TakeDamage(int damageAmount)
    {
        particle.Play();
        life -= damageAmount;
        if (life <= 0)
        {
            enemyCountController.EnemyKilled();

            Destroy(gameObject);
        }
    }
}
