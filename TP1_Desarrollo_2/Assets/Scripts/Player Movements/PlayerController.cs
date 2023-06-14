using UnityEngine;

/// <summary>
/// Class of All Functions Logic of Movement from the Player
/// </summary>
public class PlayerController : MonoBehaviour, IPlayerActions
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private float speedSprinting = 1;

    private Vector3 movement;
    private Vector3 currentMovement;

    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Rigidbody is null! Disabling PlayerController.");
            enabled = false;
        }
    }

    public void Move(Vector2 direction)
    {
        movement = direction;      
    }

    public void SetSprint(bool isSprinting)
    {
        if (isSprinting)
        {
            speedSprinting = 1.2f;
        }
        else
        {
            speedSprinting = 1;
        }
    }

    private void Update()
    {
        currentMovement = (movement.y * transform.forward) + (movement.x * transform.right);
    }

    private void FixedUpdate()
    {
        rb.velocity = currentMovement * speed * speedSprinting + Vector3.up * rb.velocity.y * Time.deltaTime;
    }
}
