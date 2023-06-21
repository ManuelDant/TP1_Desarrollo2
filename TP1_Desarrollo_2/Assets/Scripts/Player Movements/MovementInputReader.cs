using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class to initialize the player movement functions
/// </summary>
public class MovementInputReader : MonoBehaviour
{
    private IPlayerActions playerActions;

    private void Awake()
    {
        playerActions = GetComponent<IPlayerActions>();
    }
    /// <summary>
    /// The method receives input from InputSystem for Movement from the player.
    /// </summary>
    public void OnMove(InputValue context)
    {
        playerActions.Move(context.Get<Vector2>());
    }
}
