using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWeaponManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource reloadSound;

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void EnableSounds()
    {
        shootSound.enabled = true;
        reloadSound.enabled = true;
    }

    public void PlayReloadSound()
    {
        reloadSound.Play();
    }
}
