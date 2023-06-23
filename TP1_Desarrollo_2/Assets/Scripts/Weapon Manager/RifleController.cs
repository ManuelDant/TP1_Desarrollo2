using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller of Rifle weapon
/// </summary>
public class RifleController : WeaponInputManager
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float fireRate = 0.2f;

    protected override string GetWeaponType()
    {
        return "Rifle";
    }

    /// <summary>
    /// Perform all the logic of shoot the weapon
    /// </summary>
    /// <returns>The speed at which it takes to repeat the coroutine</returns>
    private IEnumerator ShootCoroutine()
    {
        while (!isDropped && currentAmmo > 0)
        {
            particleWeapon.ParticlePlay();
            animatorWeapon.SetTriggerAnimator("Shoot");
            currentAmmo--;
            uiMunition.UpdateAmmoText(currentAmmo,isDropped);

            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    EnemyHealthManager enemy = hit.collider.gameObject.GetComponent<EnemyHealthManager>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }
            soundWeaponManager.PlayShootSound();

            yield return new WaitForSeconds(fireRate);
        }
    }



    /// <summary>
    /// Shoot the weapon repeatedly when interacting with the input    
    /// </summary>
    /// <param name="input">Input information</param>
    public void OnRapidShoot(InputValue input)
    {
        if (!isDropped)
        {
            if (input.isPressed)
            {
                StopAllCoroutines();
                StartCoroutine(ShootCoroutine());
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }
}