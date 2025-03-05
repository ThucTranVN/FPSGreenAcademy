using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : BasePickup
{
    [SerializeField] WeaponSO weaponSO;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
