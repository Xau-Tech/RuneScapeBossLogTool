using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract class for any item that degrades with use
public abstract class DegradableEquipment : EquippedItem
{
    public DegradableEquipment(DegradableArmourSO armourData) : base(armourData) { }
    public DegradableEquipment(DegradableWeaponSO weaponData) : base(weaponData) { }

    public int maxCharges { get; private set; }

    public override ulong GetValue()
    {
        if (isEquipped)
            return 0;
        else
        {
            //  TODO
            throw new System.NotImplementedException();
        }
    }
}
