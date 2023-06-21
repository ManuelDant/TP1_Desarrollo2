using UnityEngine;

/// <summary>
/// Movevement strategy change manager
/// </summary>
public class EnemyMovementStrategysManager : MonoBehaviour
{
    private IMovementStrategy movementStrategy;
    private MovementStrategyType movementType;

    [SerializeField] private int timeMovement = 0;
    [SerializeField] private float forceJump = 0f;

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
        movementStrategy.Move(transform, ref moveDelay, timeMovement, forceJump, animator);
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
            case MovementStrategyType.Random:
                movementStrategy = gameObject.GetComponent<RandomMovementStrategy>();
                break;
            case MovementStrategyType.Jump:
                movementStrategy = gameObject.GetComponent<JumpMovementStrategy>();
                break;
            case MovementStrategyType.Stationary:
                movementStrategy = gameObject.GetComponent<StationaryMovementStrategy>();
                break;
        }
    }
}
