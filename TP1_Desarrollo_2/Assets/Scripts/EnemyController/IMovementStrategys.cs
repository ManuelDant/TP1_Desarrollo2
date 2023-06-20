using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSetup
{
    public interface IMovementStrategy
    {
        /// <summary>
        /// Moves the object according to the specific movement strategy.
        /// </summary>
        /// <param name="transform">The transform of the object to move.</param>
        /// <param name="moveDelay">The delay for movement.</param>
        /// <param name="timeMovement">The time for movement.</param>
        /// <param name="forceJump">The force for jumping.</param>
        /// <param name="animator">The animator component for animation control.</param>
        void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator);
    }

    public enum MovementStrategyType
    {
        Random,
        Jump,
        Stationary
    }
}