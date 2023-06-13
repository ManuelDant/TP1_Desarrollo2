using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprint : MonoBehaviour
{
    private IPlayerActions playerActions;
    private bool sprintVelocity = false;

    private void Awake()
    {
        playerActions = GetComponent<IPlayerActions>();
    }

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