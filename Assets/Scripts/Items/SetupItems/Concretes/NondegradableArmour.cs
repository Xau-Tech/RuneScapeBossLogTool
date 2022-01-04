using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Nondegradable armour setup item
/// </summary>
public class NondegradableArmour : Armour
{
    //  Constructor

    public NondegradableArmour(NondegradeArmourSO armourData) : base(armourData) { }

    //  Methods

    public override ulong GetValue()
    {
        return 0;
    }
}
