using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrayerRestoreEffect", menuName = "Setup/ItemEffects/PrayerRestoreEffect", order = 0)]
public class PrayerRestoreEffect : AbstractItemEffect
{
    [SerializeField] private short baseRestoreAmount;
    [SerializeField] private float levelMultiplierPercent;

    public override void Apply(Player player)
    {
        player.prayerRestored += Mathf.RoundToInt(baseRestoreAmount + (levelMultiplierPercent * player.GetSkillLevel(SkillLoaderArray.SkillNames.Prayer) * 10));
    }
}
