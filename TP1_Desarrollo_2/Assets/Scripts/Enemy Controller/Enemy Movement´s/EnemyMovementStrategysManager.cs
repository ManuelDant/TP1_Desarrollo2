using UnityEngine;

/// <summary>
/// Movevement strategy change manager
/// </summary>
public class EnemyMovementStrategysManager : MonoBehaviour
{
    private IMovementStrategy movementStrategy;
    private MovementStrategyType movementType;

    [SerializeField] private int speed = 0;

    private Animator animator;
    private float moveDelay = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetMovementStrategy(movementType);  
    }

    private void FixedUpdate()
    {
        moveDelay += Time.fixedDeltaTime;
        movementStrategy.Move(transform, ref moveDelay, speed, animator);
    }

    /// <summary>
    /// Sets the movement strategy for the enemy.
    /// </summary>
    /// <param name="strategy">The movement strategy to set.</param>
    public void SetMovementStrategy(MovementStrategyType strategy)
    {
        movementType = strategy;

        switch (strategy)
        {
            case MovementStrategyType.Stationary:
                movementStrategy = gameObject.GetComponent<StationaryMovementStrategy>();
                break;
            case MovementStrategyType.Patrol:
                movementStrategy = gameObject.GetComponent<PatrolMovementStrategy>();
                break;
            case MovementStrategyType.Random:
                movementStrategy = gameObject.GetComponent<RandomMovementStrategy>();
                break;
        }
    }
}
