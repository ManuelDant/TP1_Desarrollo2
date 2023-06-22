using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RifleController : WeaponPositionManager
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float fireRate = 0.2f;

    protected override string GetWeaponType()
    {
        return "Rifle";
    }

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
                        enemy.TakeDamage(10);
                    }
                }
            }
            soundWeaponManager.PlayShootSound();

            yield return new WaitForSeconds(fireRate);
        }
    }

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