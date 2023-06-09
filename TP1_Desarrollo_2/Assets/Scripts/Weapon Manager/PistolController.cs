using UnityEngine;

/// <summary>
/// Controller of Pistol weapon
/// </summary>
public class PistolController : WeaponInputAndPositionManager
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float velocityProjectile = 50;
    [SerializeField] private float projectileLifetime = 2f;

    /// <summary>
    /// Shoot weapon when interacting with input
    /// </summary>
    public void OnShoot()
    {
        if (currentAmmo > 0 && !isDropped)
        {
            currentAmmo--;
            uiMunition.UpdateAmmoText(currentAmmo, isDropped);
            particleWeapon.ParticlePlay();
            animatorWeapon.SetTriggerAnimator("Shoot");

            
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);        
            Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Ray ray = cam.ScreenPointToRay(centerScreen);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direccionDisparo = (hit.point - transform.position).normalized;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(direccionDisparo * velocityProjectile, ForceMode.Impulse);
            }

            soundWeaponManager.PlayShootSound();

            Destroy(bullet, projectileLifetime);
        }
    }
}
