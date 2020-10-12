using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Weapon augmented using the invention skill
public class AugmentedWeapon : Weapon
{
    public AugmentedWeapon(AugWeaponSO weaponData) : base(weaponData) { }

    public static readonly float MAXCHARGES = 100000.0f;

    public override ulong GetValue()
    {
        return 0;
    }
}
