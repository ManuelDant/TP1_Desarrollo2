using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCamara : MonoBehaviour
{
    [SerializeField]
    private Vector2 rotationSpeed;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // bloquea el cursor en el centro de la pantalla
    }

    public void OnCamera(InputValue context)
    {
        var rotationDelta = context.Get<Vector2>();
        RotationCamera(rotationDelta);
    }

    private void RotationCamera(Vector2 delta)
    {
        Vector2 scaledDelta = Vector2.Scale(delta, rotationSpeed) * Time.deltaTime;

        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x -= scaledDelta.y;
        eulerRotation.y += scaledDelta.x;
        eulerRotation.z = 0f;
        transform.rotation = Quaternion.Euler(eulerRotation);

  
    }
}