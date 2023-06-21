using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInfo
{
    public enum DamageTypeCategory
    {
        Normal = 0,
        Bleed,
        Channeled
    }
    public enum AbilityTypeCategory
    {
        Basic = 0,
        Threshold,
        Ultimate,
        Spec
    }
}
