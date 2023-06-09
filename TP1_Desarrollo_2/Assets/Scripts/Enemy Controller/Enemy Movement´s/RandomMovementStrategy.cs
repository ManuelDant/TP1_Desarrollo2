using UnityEngine;

/// <summary>
/// Moves the enemy randomly from side to side according to speed.
/// </summary>
public class RandomMovementStrategy : MonoBehaviour, IMovementStrategy
{
    private AnimatorFloatsManager animatorFloatsManager;

    private void Start()
    {
        animatorFloatsManager = GetComponent<AnimatorFloatsManager>();
    }

    public void Move(Transform transform, ref float moveDelay, int speed, Animator animator)
    {
        moveDelay += Time.deltaTime * 1;
        Vector3 randomPosition = new Vector3(0, 0, Mathf.Sin(moveDelay)) * speed;

        transform.position += randomPosition * Time.deltaTime;
        animatorFloatsManager.SetFloats(transform.position.x, 0, animator);

        if (randomPosition != Vector3.zero)
        {
            transform.LookAt(transform.position + randomPosition);
        }
    }

}
