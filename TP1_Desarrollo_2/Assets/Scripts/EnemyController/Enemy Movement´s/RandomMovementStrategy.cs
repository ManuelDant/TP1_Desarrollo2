using UnityEngine;

/// <summary>
/// Moves the enemy randomly.
/// </summary>
public class RandomMovementStrategy : MonoBehaviour, IMovementStrategy
{
    private AnimatorFloatsManager animatorFloatsManager;
    private void Start()
    {
        animatorFloatsManager = GetComponent<AnimatorFloatsManager>();
    }

    public void Move(Transform transform, ref float moveDelay, int timeMovement, float forceJump, Animator animator)
    {
        moveDelay += Time.deltaTime * 1;
        Vector3 randomPosition = new Vector3(0f, 0f, Mathf.Sin(moveDelay)) * timeMovement;
        transform.position += randomPosition * Time.deltaTime;
        animatorFloatsManager.SetFloats(transform.position.x, 0, animator);

        // Orient the object towards the direction of movement
        if (randomPosition != Vector3.zero)
        {
            transform.LookAt(transform.position + randomPosition);
        }
    }
}