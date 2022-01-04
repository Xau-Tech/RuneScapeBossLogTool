using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any weapon setup item that degrades using an internal charge
/// </summary>
public class DegradableWeapon : Weapon
{
    //  Properties & fields

    private int _maxCharges;
    private bool _hasSmithCostReduction;

    //  Constructor

    public DegradableWeapon(DegradableWeaponSO weaponData) : base(weaponData)
    {
        _maxCharges = weaponData._effectiveCharges;
        _hasSmithCostReduction = weaponData._smithingCostReduction;
    }

    //  Methods

    public override ulong GetValue()
    {
        if (!IsEquipped)
        {
            return 0;
        }
        else
        {
            float repairCost;

            //  Some degradable items repair cost is reduced by smith lvl - modCost = (1-(smithlvl / 200)) * baseCost
            if (_hasSmithCostReduction)
                repairCost = (1 - (ApplicationController.Instance.CurrentSetup.Player.SmithingLevel / 200.0f)) * Price;
            else
                repairCost = Price;

            //  Percent of the total charges drained in one hour at the current CombatIntensity
            float percentDrained = ApplicationController.Instance.CurrentSetup.DegradePerHour / _maxCharges;

            //  Percent drained times the modified repair cost if it were at 0%
            return (ulong)(Mathf.RoundToInt(percentDrained * repairCost));
        }
    }
}
