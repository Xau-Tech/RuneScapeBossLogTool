using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any weapon setup item that doesn't degrade
/// </summary>
public class NondegradableWeapon : Weapon
{
    //  Constructor

    public NondegradableWeapon(NondegradeWeaponSO weaponData) : base(weaponData) { }

    //  Methods

    public override ulong GetValue()
    {
        return 0;
    }
}
