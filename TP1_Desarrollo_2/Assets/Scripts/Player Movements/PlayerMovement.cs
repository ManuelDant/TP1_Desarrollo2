using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class to initialize the player movement functions
/// </summary>
//TODO: Fix - Unclear name - Could be called MovementInputReader or PlayerMovementInputHandler, etc.
public class PlayerMovement : MonoBehaviour
{
    //TODO: Fix - Unclear logic - Could this be an obsolete variable?
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
            Debug.LogError("Rigidbody is null! Disabling PlayerMovement.");
            enabled = false;
        }
    }
    /// <summary>
    /// The method receives input from InputSystem for Movement from the player.
    /// </summary>
    public void OnMove(InputValue context)
    {
        playerActions.Move(context.Get<Vector2>());
    }
}
