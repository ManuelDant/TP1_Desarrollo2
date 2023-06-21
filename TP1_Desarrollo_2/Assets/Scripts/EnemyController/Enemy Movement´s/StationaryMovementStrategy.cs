using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps the enemy stationary.
/// </summary>
public class StationaryMovementStrategy : MonoBehaviour, IMovementStrategy
{
    private AnimatorFloatsManager animatorFloatsManager;

    private void Start()
    {
        animatorFloatsManager = GetComponent<AnimatorFloatsManager>();
    }
    
    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        transform.position += new Vector3(0, 0, 0);
        animatorFloatsManager.SetFloats(0, 0, animator);
    }

}
