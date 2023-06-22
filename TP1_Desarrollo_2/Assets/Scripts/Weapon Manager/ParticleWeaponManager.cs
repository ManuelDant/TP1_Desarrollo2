using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWeaponManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    public void ParticlePlay()
    {
        particle.Play();
    }
}
