using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : MonoBehaviour
{
    [SerializeField] private string TagEnemy = "Enemy";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnemy))
        {
            if (other.TryGetComponent(out EnemyHealthManager enemy))
            {
                enemy.TakeDamage(10);
            }

            Destroy(gameObject);
        }
    }

}
