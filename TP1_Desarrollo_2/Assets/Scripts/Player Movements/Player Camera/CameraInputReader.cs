using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Detect the input of the player's camera movement
/// </summary>
public class CameraInputReader : MonoBehaviour
{
    private ICameraRotation cameraRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Initialize the camera rotation strategy
        cameraRotation = gameObject.GetComponent<CameraRotationManager>();
    }

    /// <summary>
    /// Detect camera input
    /// </summary>
    /// <param name="context">The information of the movement of the input</param>
    public void OnCamera(InputValue context)
    {
        Vector2 rotationDelta = context.Get<Vector2>();
        cameraRotation.RotateCamera(rotationDelta);
    }
}