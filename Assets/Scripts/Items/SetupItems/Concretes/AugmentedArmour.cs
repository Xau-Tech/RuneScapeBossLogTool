using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Augmented armour setup item
/// </summary>
public class AugmentedArmour : Armour
{
    //  Properties & fields

    public static readonly float MAXCHARGES = 100000.0f;

    //  Constructor

    public AugmentedArmour(AugArmourSO armourData) : base(armourData) { }

    //  Methods

    public override ulong GetValue()
    {
        return 0;
    }
}
