using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Armour that degrades purely based on time active
public class TimeDegradeArmour : Armour
{
    private int maxChargeInHours;

    public TimeDegradeArmour(TimeDegradeArmourSO armourData) : base(armourData)
    {
        this.maxChargeInHours = armourData.maxChargeInHours;
    }

    public override ulong GetValue()
    {
        if (!isEquipped)
            return 0;
        else
        {
            //  Percent of the total charges drained in one hour
            float percentDrained = 1.0f / maxChargeInHours;

            //  Percent drained times the modified repair cost if it were at 0%
            return (ulong)(Mathf.RoundToInt(percentDrained * base.price));
        }
    }
}
