using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPositionManager : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] protected int maxAmmo = 30;
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

        if (transform.parent != originParent)
            OnDropWeapon();
    }

    

    public virtual void OnReload()
    {
        if (currentAmmo < maxAmmo && !isDropped)
        {
            currentAmmo = maxAmmo;
            uiMunition.UpdateAmmoText(currentAmmo, isDropped);
            soundWeaponManager.PlayReloadSound();
            GetComponent<PlayerInput>().enabled = false;
            Invoke("EnablePlayerInput", 1.3f);
            animatorWeapon.SetTriggerAnimator("Reload");
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

    public void OnPickUpWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        WeaponPositionManager currentWeapon = player.GetComponentInChildren<WeaponPositionManager>(); 

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

    protected virtual string GetWeaponType()
    {
        return "Weapon";
    }
}