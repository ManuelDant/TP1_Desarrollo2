using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Pistol;

public class ScriptRifle : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField]
    private int maxAmmo = 30;
    private int currentAmmo;

    [Header("UI Settings")]
    [SerializeField]
    private TextMeshProUGUI ammoText;

    private bool isDropped = false;
    private GameObject newParent;

    [Header("Position Gun")]
    [SerializeField]
    private GameObject positionGunObject;
    [SerializeField]
    private Transform originParent;
    [SerializeField]
    private Camera cam;
    
    [Header("Particle")]
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private Animator animator;

    [Header("Sound")]
    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private AudioSource reloadSound;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();

        if (transform.parent != originParent)
            OnDropWeapon();
    }

    void UpdateAmmoText()
    {
        if (!isDropped)
        {
            ammoText.text = "" + currentAmmo;
        }
        else
        {
            ammoText.text = "";
        }
    }

    public void OnReload()
    {
        if (currentAmmo < maxAmmo && !isDropped)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();
            reloadSound.Play();
            GetComponent<PlayerInput>().enabled = false; // Desactiva el componente PlayerInput
            Invoke("EnablePlayerInput", 1);
            animator.SetTrigger("Reload");
        }
    }

    void EnablePlayerInput()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    public void OnDropWeapon()
    {
        if (isDropped) // Verificar si el arma ya ha sido soltada
        {
            return; // Si es así, salir de la función
        }

        animator.enabled = false;
        isDropped = true;
        UpdateAmmoText();

        // Crear un nuevo objeto padre si no existe
        if (newParent == null)
        {
            newParent = new GameObject("WeaponParent");
        }

        // Desvincular el objeto del arma del objeto padre original
        transform.parent = null;

        // Vincular el objeto del arma al nuevo objeto padre
        transform.parent = newParent.transform;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        GetComponent<BoxCollider>().enabled = true;
    }

    public void OnPickUpWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Encontrar al jugador
        GameObject existingWeapon = FindWeaponInChildren(player.transform);
        if (existingWeapon != null)
        {
            return;
            //pistol.OnDropWeapon(); // Llamar el método OnPickUpWeapon() en el script de la otra arma
        }

        float radius = 2f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                isDropped = false;
                UpdateAmmoText();

                if (newParent != null)
                {
                    Destroy(newParent);
                }

                transform.parent = originParent;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }
                GetComponent<PlayerInput>().enabled = true;
                GetComponent<BoxCollider>().enabled = false;
                animator.enabled = true;

                transform.position = positionGunObject.transform.position;
                transform.rotation = positionGunObject.transform.rotation;
                positionGunObject.SetActive(false);
                shootSound.enabled = true;
                reloadSound.enabled = true;
            }
        }
    }

    GameObject FindWeaponInChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("Weapon"))
            {
                return child.gameObject;
            }
            else
            {
                GameObject found = FindWeaponInChildren(child);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }

    public void OnShoot(InputValue input)
    {
        
        if (!isDropped && currentAmmo > 0) // verificar que el arma no se haya soltado y que haya balas disponibles
        {
            if (input.isPressed)
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
                        Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                        if (enemy != null)
                        {
                            enemy.TakeDamage(10);
                            enemy.Invoke("PlaySound", 0.1f);
                        }
                    }
                }
                shootSound.Play();
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar un rayo desde la posición del transform hacia adelante
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * 100f); // el valor 100f es la longitud del rayo

        // Dibujar un punto en el punto de impacto del raycast
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.1f); // el valor 0.1f es el tamaño del punto
        }
    }
}