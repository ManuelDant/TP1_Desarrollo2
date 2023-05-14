using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente Enemy del objeto con el que se colisionó
            Enemy enemy = other.GetComponent<Enemy>();

            // Hacer que el enemigo pierda vida
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                enemy.Invoke("PlaySound", 0.1f);
            }

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }

}
