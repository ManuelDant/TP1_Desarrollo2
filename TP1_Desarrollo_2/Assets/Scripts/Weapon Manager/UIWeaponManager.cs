using TMPro;
using UnityEngine;

/// <summary>
/// Controller of the UI of the weapon
/// </summary>
public class UIWeaponManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI ammoText;

    /// <summary>
    /// Updates the UI of the weapon's ammo on the moment
    /// </summary>
    /// <param name="currentAmmo">Amount of ammo currently</param>
    /// <param name="isDropped">Boolean that checks if the weapon is dropped</param>
    public void UpdateAmmoText(float currentAmmo, bool isDropped)
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
}
