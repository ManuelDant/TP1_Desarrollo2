using UnityEngine;

/// <summary>
/// Change enemy movement
/// </summary>
public class EnemySetMovementManager : MonoBehaviour
{
    [SerializeField] private MovementStrategyType movementStrategy;

    private EnemyMovementStrategysManager movementManager;

    private void Start()
    {
        movementManager = GetComponent<EnemyMovementStrategysManager>();        
    }

    private void Update()
    {
        SetMovementStrategy(movementStrategy);
    }

    /// <summary>
    /// Sets the enemy's movement strategy.
    /// </summary>
    /// <param name="strategy">The movement strategy to set.</param>
    public void SetMovementStrategy(MovementStrategyType strategy)
    {
        movementStrategy = strategy;

        if (movementManager != null)
        {
            movementManager.SetMovementStrategy(strategy);
        }
    }
}
