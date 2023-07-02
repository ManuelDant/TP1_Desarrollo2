using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller of the movement of the player's camera through the information of the inputs
/// </summary>
public class CameraRotationManager : MonoBehaviour, ICameraRotation
{
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private float controllerSensitivity = 50f;
    [SerializeField] private float sensitivityChangeDelay = 1f; // Delay before changing sensitivity (in seconds)

    private float sensitivity;
    private float timeSinceLastMouseMove;
    private bool isMouseMoving;

    private Transform playerBody;
    private Vector2 previousMousePosition;

    private void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        previousMousePosition = Mouse.current.position.ReadValue();
    }

    private float xRotation = 0f;

    public void RotateCamera(Vector2 delta)
    {
        float mouseX = delta.x * sensitivity * Time.deltaTime;
        float mouseY = delta.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up, mouseX);
    }

    private void Update()
    {
        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        if (currentMousePosition != previousMousePosition)
        {
            isMouseMoving = true;
            timeSinceLastMouseMove = 0f;
        }
        else
        {
            isMouseMoving = false;
            timeSinceLastMouseMove += Time.deltaTime;
        }

        if (isMouseMoving)
        {
            sensitivity = mouseSensitivity;
        }
        else if (timeSinceLastMouseMove >= sensitivityChangeDelay)
        {
            sensitivity = controllerSensitivity; //if the mouse dont move, increase the sensitivity for the joystick.
        }

        previousMousePosition = currentMousePosition;     
    }
}
