using UnityEngine;

/// <summary>
/// Manager Particle of the enemy
/// </summary>
public class EnemyParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// Activate the particle of the enemy
    /// </summary>
    public void PlayParticle()
    {
        particle.Play();
    }
}
