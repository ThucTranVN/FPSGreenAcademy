using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : BasePickup
{
    [SerializeField] int ammoAmount = 10;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
