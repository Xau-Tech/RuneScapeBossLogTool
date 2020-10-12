using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Armour that has been augmented using the invention skill
public class AugmentedArmour : Armour
{
    public AugmentedArmour(AugArmourSO armourData) : base(armourData) { }

    public static readonly float MAXCHARGES = 100000.0f;

    public override ulong GetValue()
    {
        return 0;
    }
}
