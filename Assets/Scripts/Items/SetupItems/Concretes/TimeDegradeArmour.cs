using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time degrade armour setup item
/// </summary>
public class TimeDegradeArmour : Armour
{
    //  Properties & fields

    private int _maxChargeInHours;

    //  Constructor

    public TimeDegradeArmour(TimeDegradeArmourSO tda) : base(tda)
    {
        this._maxChargeInHours = tda._maxChargeInHours;
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
            //  Percent of the total charges drained in one hour
            float percentDrained = 1.0f / _maxChargeInHours;

            //  Percent drained times the modified repair cost if it were at 0%
            return (ulong)(Mathf.RoundToInt(percentDrained * Price));
        }
    }
}
