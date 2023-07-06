using UnityEngine;

/// <summary>
/// Moves the enemy by patrol using waypoints.
/// </summary>
public class PatrolMovementStrategy : MonoBehaviour, IMovementStrategy
{
    [SerializeField] private Transform TargetWayPoint;

    private AnimatorFloatsManager animatorFloatsManager;

    private void Start()
    {       
        animatorFloatsManager = GetComponent<AnimatorFloatsManager>();
    }

    public void Move(Transform transform, ref float moveDelay, int timeMovement, Animator animator)
    {
        transform.LookAt(new Vector3(TargetWayPoint.position.x, transform.position.y, TargetWayPoint.position.z));
        transform.Translate(new Vector3(0,0,timeMovement * Time.deltaTime));
        animatorFloatsManager.SetFloats(transform.position.x, 0, animator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="WayPoint")
        {
            TargetWayPoint = other.gameObject.GetComponent<NextWayPoint>().nextPoint;
            transform.LookAt(new Vector3(TargetWayPoint.position.x, transform.position.y, TargetWayPoint.position.z));
        }
    }

}