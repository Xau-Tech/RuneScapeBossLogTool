using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Armour that degrades using an internal charge system
public class DegradableArmour : Armour
{
    private int maxCharges;
    private bool smithingCostReduction;

    public DegradableArmour(DegradableArmourSO armourData) : base(armourData)
    {
        maxCharges = armourData.effectiveCharges;
        smithingCostReduction = armourData.smithingCostReduction;
    }

    public override ulong GetValue()
    {
        if (!isEquipped)
        {
            Debug.Log($"{itemName} is not equipped");
            return 0;
        }
        else
        {
            float repairCost;

            //  Some degradable items repair cost is reduced based on Smithing level where modCost = (1-(smithlvl / 200)) * baseCost
            if (smithingCostReduction)
                repairCost = (1 - (CacheManager.SetupTab.Setup.Player.SmithingLevel / 200.0f)) * price;
            else
                repairCost = price;

            //  Percent of the total charges drained in one hour at the current CombatIntensity
            float percentDrained = CacheManager.SetupTab.Setup.DegradePerHour / maxCharges;

            //  Percent drained times the modified repair cost if it were at 0%
            return (ulong)(Mathf.RoundToInt(percentDrained * repairCost));
        }
    }
}
