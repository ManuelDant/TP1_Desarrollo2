using UnityEngine;

/// <summary>
/// Calculate the impact of the projectile to the enemy
/// </summary>
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private string TagEnemy = "Enemy";
    [SerializeField] private int damageProjectile = 10;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnemy))
        {
            if (other.TryGetComponent(out EnemyHealthManager enemy))
            {
                enemy.TakeDamage(damageProjectile);
            }

            Destroy(gameObject);
        }
    }
}
