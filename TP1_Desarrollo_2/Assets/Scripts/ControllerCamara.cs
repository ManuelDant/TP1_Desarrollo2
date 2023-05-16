using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCamara : MonoBehaviour
{
    [SerializeField]
    private float mouseSensivity = 5f;

    private float xRotation = 0f;

    [SerializeField]
    private Transform playerbody;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // bloquea el cursor en el centro de la pantalla
        if (!playerbody)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Transform>();
        }
    }

    public void OnCamera(InputValue context)
    {
        var rotationDelta = context.Get<Vector2>();
        RotationCamera(rotationDelta);
    }

    private void RotationCamera(Vector2 delta)
    {
        float mouseX = delta.x * mouseSensivity * Time.deltaTime;
        float mouseY = delta.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerbody.Rotate(Vector3.up, mouseX);  
    }
}