using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] protected int maxAmmo = 30;
    protected int currentAmmo;

    [Header("UI Settings")]
    [SerializeField] protected TextMeshProUGUI ammoText;

    protected bool isDropped = false;
    protected GameObject newParent;

    [Header("Position Gun")]
    [SerializeField] protected GameObject positionGunObject;
    [SerializeField] protected Transform originParent;
    [SerializeField] protected Camera cam;

    [Header("Particle")]
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected Animator animator;

    [Header("Sound")]
    [SerializeField] protected AudioSource shootSound;
    [SerializeField] protected AudioSource reloadSound;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();

        if (transform.parent != originParent)
            OnDropWeapon();
    }

    protected void UpdateAmmoText()
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

    public virtual void OnReload()
    {
        if (currentAmmo < maxAmmo && !isDropped)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();
            reloadSound.Play();
            GetComponent<PlayerInput>().enabled = false;
            Invoke("EnablePlayerInput", 1.3f);
            animator.SetTrigger("Reload");
        }
    }

    protected void EnablePlayerInput()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    public void OnDropWeapon()
    {
        if (isDropped)
        {
            return;
        }
        animator.enabled = false;

        isDropped = true;
        UpdateAmmoText();


        if (newParent == null)
        {
            newParent = new GameObject("WeaponPlaceHolder");
        }

        transform.parent = null;
        transform.parent = newParent.transform;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        GetComponent<BoxCollider>().enabled = true;
    }

    public void OnPickUpWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Weapon currentWeapon = player.GetComponentInChildren<Weapon>(); 

        if (currentWeapon != null)
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

    protected virtual string GetWeaponType()
    {
        return "Weapon";
    }
}