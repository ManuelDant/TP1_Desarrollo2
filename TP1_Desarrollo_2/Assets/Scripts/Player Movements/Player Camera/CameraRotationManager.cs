using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the sensitivity and rotation of the player's camera
/// </summary>
public class CameraRotationManager : MonoBehaviour, ICameraRotation
{
    [SerializeField] private float mouseSensitivity = 5f;
    private Transform playerBody;
    private void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private float xRotation = 0f;
    public void RotateCamera(Vector2 delta)
    {
        float mouseX = delta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = delta.y * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up, mouseX);
    }
}
