using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Degradable armour setup item
/// </summary>
public class DegradableArmour : Armour
{
    //  Properties & fields

    private int _maxCharges;
    private bool _hasSmithCostReduction;

    //  Constructor

    public DegradableArmour(DegradableArmourSO da) : base(da)
    {
        this._maxCharges = da._effectiveCharges;
        this._hasSmithCostReduction = da._hasSmithCostReduction;
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
