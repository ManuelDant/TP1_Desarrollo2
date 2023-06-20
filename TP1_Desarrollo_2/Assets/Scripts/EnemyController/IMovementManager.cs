using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMovementManager : MonoBehaviour
{
    private EnemyMovementSetup.IMovementStrategy movementStrategy;
    private EnemyMovementSetup.MovementStrategyType movementType;

    [SerializeField] private int timeMovement = 0;
    [SerializeField] private float forceJump = 0f;
    private Animator animator;
    private float moveDelay = 0f;

    private void Start()
    {
        SetMovementStrategy(movementType);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        moveDelay += Time.deltaTime * 1;
        movementStrategy.Move(transform, ref moveDelay, timeMovement, forceJump, animator);
    }

    /// <summary>
    /// Sets the movement strategy for the enemy.
    /// </summary>
    /// <param name="strategy">The movement strategy to set.</param>
    public void SetMovementStrategy(EnemyMovementSetup.MovementStrategyType strategy)
    {
        movementType = strategy;

        switch (strategy)
        {
            case EnemyMovementSetup.MovementStrategyType.Random:
                movementStrategy = new RandomMovementStrategy();
                break;
            case EnemyMovementSetup.MovementStrategyType.Jump:
                movementStrategy = new JumpMovementStrategy();
                break;
            case EnemyMovementSetup.MovementStrategyType.Stationary:
                movementStrategy = new StationaryMovementStrategy();
                break;
        }
    }

    /// <summary>
    /// Notifies the movement strategy has changed.
    /// </summary>
    /// <param name="strategy">The new movement strategy.</param>
    public void OnMovementStrategyChanged(EnemyMovementSetup.MovementStrategyType strategy)
    {
        SetMovementStrategy(strategy);
    }
}

public class RandomMovementStrategy : EnemyMovementSetup.IMovementStrategy
{
    /// <summary>
    /// Moves the enemy randomly.
    /// </summary>
    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        moveDelay += Time.deltaTime * 1;
        Vector3 randomPosition = new Vector3(0f, 0f, Mathf.Sin(moveDelay)) * timeMovement;
        transform.position += randomPosition * Time.deltaTime;
        SetFloats(transform.position.x, 0, animator);

        // Orient the object towards the direction of movement
        if (randomPosition != Vector3.zero)
        {
            transform.LookAt(transform.position + randomPosition);
        }
    }

    private void SetFloats(float x, float y, Animator animator)
    {
        animator.SetFloat("velX", x);
        animator.SetFloat("velY", y);
    }
}

public class JumpMovementStrategy : EnemyMovementSetup.IMovementStrategy
{
    /// <summary>
    /// Moves the enemy by jumping.
    /// </summary>
    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        if (moveDelay < 3)
        {
            animator.SetBool("Jump", false);
        }

        if (moveDelay >= 3)
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            animator.SetBool("Jump", true);
            moveDelay = 0;
        }
    }
}

public class StationaryMovementStrategy : EnemyMovementSetup.IMovementStrategy
{
    /// <summary>
    /// Keeps the enemy stationary.
    /// </summary>
    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        transform.position += new Vector3(0, 0, 0);
        SetFloats(0, 0, animator);
    }

    private void SetFloats(float x, float y, Animator animator)
    {
        animator.SetFloat("velX", x);
        animator.SetFloat("velY", y);
    }
}
