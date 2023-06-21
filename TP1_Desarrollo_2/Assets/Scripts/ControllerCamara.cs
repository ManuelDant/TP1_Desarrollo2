using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: TP2 - Syntax - Consistency in naming convention
public class ControllerCamara : MonoBehaviour
{
    [SerializeField]
    private float mouseSensivity = 5f;

    //TODO: TP2 - Syntax - Fix declaration order
    private float xRotation = 0f;

    [SerializeField]
    private Transform playerbody;


    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Start()
    {
        //TODO: Fix - Code is in Spanish
        Cursor.lockState = CursorLockMode.Locked; // bloquea el cursor en el centro de la pantalla
        if (!playerbody)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //BUG: You aren't saving this value
            player.GetComponent<Transform>();
        }
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnCamera(InputValue context)
    {
        var rotationDelta = context.Get<Vector2>();
        RotationCamera(rotationDelta);
    }

    //TODO: TP2 - SOLID
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