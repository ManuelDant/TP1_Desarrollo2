using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWeaponManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI ammoText;

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
