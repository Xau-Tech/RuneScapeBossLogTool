using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any setup item weapon that degrades using the augment system
/// </summary>
public class AugmentedWeapon : Weapon
{
    //  Properties & fields

    public static readonly float MAXCHARGES = 100000.0f;

    //  Constructor

    public AugmentedWeapon(AugWeaponSO weaponData) : base(weaponData) { }

    //  Methods

    public override ulong GetValue()
    {
        return 0;
    }
}
