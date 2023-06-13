using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private IPlayerActions playerActions;

    private void Awake()
    {
        playerActions = GetComponent<IPlayerActions>();
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

    public void OnMove(InputValue context)
    {
        playerActions.Move(context.Get<Vector2>());
    }
}
