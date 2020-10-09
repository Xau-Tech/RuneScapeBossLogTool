using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Abstract class for any equipment that doesn't degrade with use
public abstract class NonDegradeEquipment : EquippedItem
{
    public NonDegradeEquipment(NondegradeWeaponSO weaponData) : base(weaponData) { }
    public NonDegradeEquipment(NondegradeArmourSO armourData) : base(armourData) { }

    //  No degradation means no cost/hr!
    public override ulong GetValue()
    {
        return 0;
    }
}
