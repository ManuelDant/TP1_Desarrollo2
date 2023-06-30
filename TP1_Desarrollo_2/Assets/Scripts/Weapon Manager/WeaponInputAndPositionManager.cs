using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Control weapon inputs and positioning by interacting with the inputs
/// </summary>
public class WeaponInputAndPositionManager : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] protected int maxAmmo = 30;

    [Header("Reload Settings")]
    [SerializeField] protected float reloadAmmoTime = 1.3f;
    [SerializeField] protected string nameAnimatorReload = "Reload";

    protected int currentAmmo;

    protected bool isDropped = false;
    protected GameObject newParent;

    protected UIWeaponManager uiMunition;
    protected SoundWeaponManager soundWeaponManager;
    protected ParticleWeaponManager particleWeapon;
    protected AnimatorWeaponManager animatorWeapon;

    [Header("Position Gun")]
    [SerializeField] protected GameObject positionGunObject;
    [SerializeField] protected Transform originParent;
    [SerializeField] protected Camera cam;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        uiMunition = GetComponent<UIWeaponManager>();
        uiMunition.UpdateAmmoText(currentAmmo,isDropped);

        soundWeaponManager = GetComponent<SoundWeaponManager>();
        particleWeapon = GetComponent<ParticleWeaponManager>();
        animatorWeapon = GetComponent<AnimatorWeaponManager>();

        if ( !uiMunition || !soundWeaponManager || !particleWeapon || !animatorWeapon)
        {
            Debug.LogError("Somekind of class weapon is null!");
        }

        if (transform.parent != originParent)
            OnDropWeapon();
    }

    /// <summary>
    /// Reloads the weapon by interacting with the input
    /// </summary>
    public virtual void OnReload()
    {
        if (currentAmmo < maxAmmo && !isDropped)
        {
            currentAmmo = maxAmmo;
            uiMunition.UpdateAmmoText(currentAmmo, isDropped);
            soundWeaponManager.PlayReloadSound();
            GetComponent<PlayerInput>().enabled = false;
            Invoke("EnablePlayerInput", reloadAmmoTime);
            animatorWeapon.SetTriggerAnimator(nameAnimatorReload);
        }
    }

    /// <summary>
    /// Activate the PlayerInput
    /// </summary>
    protected void EnablePlayerInput()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    /// <summary>
    /// Drops the weapon when interacting with the input
    /// </summary>
    public void OnDropWeapon()
    {
        if (isDropped)
        {
            return;
        }
        animatorWeapon.enabled = false;

        isDropped = true;
        uiMunition.UpdateAmmoText(currentAmmo, isDropped);


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

    /// <summary>
    /// Pick the weapon when interacting with the input
    /// </summary>
    public void OnPickUpWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        WeaponInputAndPositionManager currentWeapon = player.GetComponentInChildren<WeaponInputAndPositionManager>(); 

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
                uiMunition.UpdateAmmoText(currentAmmo, isDropped);

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
                animatorWeapon.enabled = true;

                transform.position = positionGunObject.transform.position;
                transform.rotation = positionGunObject.transform.rotation;
                positionGunObject.SetActive(false);
                soundWeaponManager.EnableSounds();
            }
        }
    }

}