using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Fix - Hardcoded value
        if (other.CompareTag("Enemy"))
        {
            //TODO: Fix - Code is in Spanish
            // Obtener el componente Enemy del objeto con el que se colision�
            EnemyController enemy = other.GetComponent<EnemyController>();

            //TODO: Fix - TryGetComponent
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
