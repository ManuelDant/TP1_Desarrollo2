using UnityEngine;

/// <summary>
/// Weapon Shoot and Reload sound controller
/// </summary>
public class SoundWeaponManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource reloadSound;
    [SerializeField] private float minPitchSound = 1.05f;
    [SerializeField] private float maxPitchSound = 0.95f;

    /// <summary>
    /// Activate Shoot sound
    /// </summary>
    public void PlayShootSound()
    {
        float pitchVariation = Random.Range(minPitchSound,maxPitchSound);
        shootSound.pitch = pitchVariation;
        shootSound.Play();
    }

    /// <summary>
    /// Enable weapon sounds
    /// </summary>
    public void EnableSounds()
    {
        shootSound.enabled = true;
        reloadSound.enabled = true;
    }

    /// <summary>
    /// Activate Reload sound
    /// </summary>
    public void PlayReloadSound()
    {
        reloadSound.Play();
    }
}
