using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class to initialize the player sprint functions
/// </summary>
public class PlayerSprint : MonoBehaviour
{
    private IPlayerActions playerActions;
    private bool sprintVelocity = false;

    private void Awake()
    {
        playerActions = GetComponent<IPlayerActions>();
    }
    /// <summary>
    /// The method receives input from InputSystem for Sprint from the player.
    /// </summary>
    public void OnSprint(InputValue input)
    {
        if (input.isPressed)
        {
            sprintVelocity = true;
        }
        else
        {
            sprintVelocity = false;
        }
        playerActions.SetSprint(sprintVelocity);
    }
}