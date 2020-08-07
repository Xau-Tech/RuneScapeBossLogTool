using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : AbsItemEffect
{
    public short baseHealAmount;

    public override void Apply()
    {
        Debug.Log($"You heal {baseHealAmount} lp!");
    }
}
