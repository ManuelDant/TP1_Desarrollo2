using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller of the particles weapon 
/// </summary>
public class ParticleWeaponManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    /// <summary>
    /// Activate weapon particle
    /// </summary>
    public void ParticlePlay()
    {
        particle.Play();
    }
}
