using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract class for any equipment augmented with the Invention skill
public abstract class AugmentedEquipment : EquippedItem
{
    public AugmentedEquipment(AugArmourSO armourData) : base(armourData) { }
    public AugmentedEquipment(AugWeaponSO weaponData) : base(weaponData) { }

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