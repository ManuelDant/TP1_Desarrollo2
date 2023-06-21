using UnityEngine;

/// <summary>
/// Calculate the impact of the projectile to the enemy
/// </summary>
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
