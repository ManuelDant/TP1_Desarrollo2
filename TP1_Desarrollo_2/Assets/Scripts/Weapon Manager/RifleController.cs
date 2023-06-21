using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RifleController : Weapon
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
            particle.Play();
            animator.SetTrigger("Shoot");
            currentAmmo--;
            UpdateAmmoText();

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
            shootSound.Play();

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