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
    
    public void Move(Transform transform, ref float moveDelay, int speed, Animator animator)
    {            
        animatorFloatsManager.SetFloats(0, 0, animator);     
    }
}
