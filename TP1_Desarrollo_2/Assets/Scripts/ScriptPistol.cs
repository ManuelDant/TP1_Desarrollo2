using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Pistol
{
    public class ScriptPistol : MonoBehaviour
    {
        [Header("Shoot Settings")]
        [SerializeField]
        private GameObject proyectil;
        [SerializeField]
        private float velocity = 20f;
        [SerializeField]
        private float timeDestroy = 2f;

        [Header("Ammo Settings")]
        [SerializeField]
        private int maxAmmo = 30;
        private int currentAmmo;

        [Header("UI Settings")]
        [SerializeField]
        private TextMeshProUGUI ammoText;

        private bool isDropped = false; // variable para saber si se soltó el arma
        private GameObject newParent; // nuevo objeto padre
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
                Invoke("EnablePlayerInput", 1.3f);
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
                newParent = new GameObject("PistolPlaceHolder");
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
            GameObject existingWeapons = FindWeaponInChildren(player.transform);
            if (existingWeapons != null)
            {
                return;
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
                    reloadSound.enabled = true;
                    shootSound.enabled = true;  
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

        public void OnShoot()
        {
            if (!isDropped && currentAmmo > 0) // verificar que el arma no se haya soltado y que haya balas disponibles
            {
                particle.Play();
                animator.SetTrigger("Shoot");
                GameObject bala = Instantiate(proyectil, transform.position, transform.rotation);

                // Obtener la posición del centro de la pantalla
                Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);

                // Hacer un raycast desde la cámara hacia el centro de la pantalla
                Ray ray = cam.ScreenPointToRay(centerScreen);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Obtener la dirección del disparo desde la posición de la bala hasta el punto hiteado por el raycast
                    Vector3 direccionDisparo = (hit.point - transform.position).normalized;

                    // Aplicar una fuerza a la bala en la dirección del disparo
                    Rigidbody rb = bala.GetComponent<Rigidbody>();
                    rb.AddForce(direccionDisparo * velocity, ForceMode.Impulse);
                }

                Destroy(bala, timeDestroy);

                shootSound.Play();

                currentAmmo--;
                UpdateAmmoText();
            }
        }

        private void OnDrawGizmos()
        {
            // Dibujar un punto en el punto de impacto del raycast
            if (Physics.Raycast(transform.position, cam.transform.forward, out RaycastHit hit))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.1f); // el valor 0.1f es el tamaño del punto
            }
        }
    }

}