using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrayerRestoreEffect", menuName = "Setup/ItemEffects/PrayerRestoreEffect", order = 0)]
public class PrayerRestoreEffect : AbstractItemEffect
{
    [SerializeField] private short baseRestoreAmount;
    [SerializeField] private float levelMultiplierPercent;

    public override void Apply(in Setup setup)
    {
        throw new System.NotImplementedException();
    }
}
