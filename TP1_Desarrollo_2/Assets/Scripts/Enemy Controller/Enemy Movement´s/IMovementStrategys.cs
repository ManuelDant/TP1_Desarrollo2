using UnityEngine;

/// <summary>
/// Actions enemy movement
/// </summary>
public interface IMovementStrategy
{
    /// <summary>
    /// Moves the object according to the specific movement strategy.
    /// </summary>
    /// <param name="transform">The transform of the object to move.</param>
    /// <param name="moveDelay">The delay for movement.</param>
    /// <param name="speed">The speed for movement.</param>
    /// <param name="animator">The animator component for animation control.</param>
    void Move(Transform transform, ref float moveDelay, int speed, Animator animator);

}

/// <summary>
/// All types of movement
/// </summary>
public enum MovementStrategyType
{
    Stationary,
    Patrol,
    Random
}
