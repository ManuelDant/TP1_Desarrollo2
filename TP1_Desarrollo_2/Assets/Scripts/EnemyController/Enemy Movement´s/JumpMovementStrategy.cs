using UnityEngine;

/// <summary>
/// Moves the enemy by jumping.
/// </summary>
public class JumpMovementStrategy : MonoBehaviour, IMovementStrategy
{
    [SerializeField] private string AnimatorJumpName = "Jump";
   
    private Rigidbody rb;

    
    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        if (moveDelay < 3)
        {
            animator.SetBool(AnimatorJumpName, false);
        }

        if (moveDelay >= 3)
        {
            if (rb == null)
            {
                rb = transform.GetComponent<Rigidbody>();
            }

            rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            animator.SetBool(AnimatorJumpName, true);
            moveDelay = 0;
        }
    }

}

